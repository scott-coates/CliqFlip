﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Common;
using CliqFlip.Infrastructure.Images;
using CliqFlip.Infrastructure.Images.Interfaces;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly IImageProcessor _imageProcessor;
		private readonly IInterestTasks _interestTasks;
		private readonly ILinqRepository<User> _repository;


		public UserTasks(ILinqRepository<User> repository, IInterestTasks interestTasks, IImageProcessor imageProcessor)
		{
			_repository = repository;
			_interestTasks = interestTasks;
			_imageProcessor = imageProcessor;
		}

		#region IUserTasks Members

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases)
		{
			//TODO: Move this data access to our infra project
			IList<string> subjAliasAndParent = _interestTasks.GetSlugAndParentSlug(interestAliases);
			var query = new AdHoc<User>(x => x.Interests.Any(y => subjAliasAndParent.Contains(y.Interest.Slug))
			                                 ||
			                                 x.Interests.Any(y => subjAliasAndParent.Contains(y.Interest.ParentInterest.Slug)));

			List<User> users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
			                            	{
			                            		MatchCount = user.Interests.Sum(x =>
			                            		                                	{
			                            		                                		if (interestAliases.Contains(x.Interest.Slug))
			                            		                                			return 3; //movies -> movies (same match)
			                            		                                		if (x.Interest.ParentInterest != null && subjAliasAndParent.Contains(x.Interest.ParentInterest.Slug))
			                            		                                			return 2; //movies -> tv shows (sibling match)
			                            		                                		if (subjAliasAndParent.Contains(x.Interest.Slug))
			                            		                                			return 1; //movies -> entertainment (parent match)
			                            		                                		return 0;
			                            		                                	}),
			                            		UserDto = new UserDto
			                            		          	{
			                            		          		Username = user.Username,
			                            		          		InterestDtos = user.Interests
			                            		          			.Select(x => new UserInterestDto(x.Interest.Id, x.Interest.Name, x.Interest.Slug)).ToList(),
			                            		          		Bio = user.Bio
			                            		          	}
			                            	}).OrderByDescending(x => x.MatchCount).ToList();
		}

		public UserDto Create(UserDto userToCreate)
		{
			var withMatchingNameOrEmail = new AdHoc<User>(x => x.Username == userToCreate.Username || x.Email == userToCreate.Email);

			//Check username and email are unique
			if (_repository.FindAll(withMatchingNameOrEmail).Any())
			{
				//TODO: this is a race condition - just let the db throw if unique violation
				return null;
			}


			string salt = PasswordHelper.GenerateSalt(16);
			string pHash = PasswordHelper.GetPasswordHash(userToCreate.Password, salt);

			var user = new User(userToCreate.Username, userToCreate.Email, pHash, salt);

			//add all the interests
			foreach (UserInterestDto userInterest in userToCreate.InterestDtos)
			{
				//get or create the Interest
				UserInterestDto userInterestDto = _interestTasks.GetOrCreate(userInterest.Name);
				user.AddInterest(new Interest(userInterestDto.Id, userInterestDto.Name), userInterest.Sociality);
			}

			user.Bio = "I ♥ " + string.Join(", ", user.Interests.Select(x => x.Interest.Name));
			user.Headline = "I am " + user.Username + ", hear me roar!";

			_repository.Save(user);
			return new UserDto {Username = user.Username, Email = user.Email, Password = user.Password};
		}


		public bool ValidateUser(string username, string password)
		{
			var withMatchingNameOrEmail = new AdHoc<User>(x => x.Username == username || x.Email == username);
			User user = _repository.FindOne(withMatchingNameOrEmail);

			if (user != null)
			{
				//use the users salt and provided password to see if the password match
				string expetectedPassword = PasswordHelper.GetPasswordHash(password, user.Salt);
				return user.Password == expetectedPassword;
			}
			return false;
		}


		public User GetUser(string username)
		{
			var adhoc = new AdHoc<User>(x => x.Username == username);
			return _repository.FindOne(adhoc);
		}

		public void SaveProfileImage(User image, HttpPostedFileBase profileImage)
		{
			//be very safe with image streams
			var exceptions = new List<Exception>();

			ImageProcessResult result = null;
			try
			{
				result = _imageProcessor.ProcessImage(profileImage);

				using (FileStream fileStream = File.Create("C:\\Test\\thumb_" + profileImage.FileName))
				{
					result.ThumbnailImage.CopyTo(fileStream);
				}
			}
			catch (Exception ex)
			{
				exceptions.Add(ex);
			}
			finally
			{
				if (result != null)
				{
					DisposeImageIfNotEmpty(result.ThumbnailImage, exceptions);
					DisposeImageIfNotEmpty(result.MediumImage, exceptions);
					DisposeImageIfNotEmpty(result.FullImage, exceptions);
				}
			}

			if (exceptions.Any())
			{
				throw new AggregateException("Error processing image", exceptions);
			}
		}

		#endregion

		private void DisposeImageIfNotEmpty(Stream streamToDispose, IList<Exception> exceptions)
		{
			if (streamToDispose != null)
			{
				try
				{
					streamToDispose.Dispose();
				}
				catch (Exception ex)
				{
					exceptions.Add(ex);
				}
			}
		}

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IEnumerable<int> interestIds)
		{
			List<int> interestList = interestIds.ToList();
			var query = new AdHoc<User>(x => x.Interests.Any(y => interestList.Contains(y.Id)));

			List<User> users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
			                            	{
			                            		MatchCount = user.Interests.Select(x => x.Id).Intersect(interestList).Count(),
			                            		UserDto = new UserDto {Username = user.Username, InterestDtos = user.Interests.Select(x => new UserInterestDto(x.Interest.Id, x.Interest.Name, x.Interest.Slug)).ToList(), Bio = user.Bio}
			                            	}).ToList();
		}
	}
}