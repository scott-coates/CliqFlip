using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Authentication.Interfaces;
using CliqFlip.Infrastructure.Common;
using CliqFlip.Infrastructure.IO;
using CliqFlip.Infrastructure.IO.Interfaces;
using CliqFlip.Infrastructure.Images;
using CliqFlip.Infrastructure.Images.Interfaces;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Validation;
using CliqFlip.Infrastructure.Web.Interfaces;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly IImageProcessor _imageProcessor;
		private readonly IInterestTasks _interestTasks;
		private readonly ILinqRepository<User> _repository;
		private readonly IFileUploadService _fileUploadService;
		private readonly IHtmlService _htmlService;
		private readonly IFeedFinder _feedFinder;
		private readonly IUserAuthentication _userAuthentication;

		public UserTasks(ILinqRepository<User> repository, IInterestTasks interestTasks, IImageProcessor imageProcessor, IFileUploadService fileUploadService, IHtmlService htmlService, IFeedFinder feedFinder, IUserAuthentication userAuthentication)
		{
			_repository = repository;
			_interestTasks = interestTasks;
			_imageProcessor = imageProcessor;
			_fileUploadService = fileUploadService;
			_htmlService = htmlService;
			_feedFinder = feedFinder;
			_userAuthentication = userAuthentication;
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

		public User Create(UserDto userToCreate)
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

			user.UpdateCreateDate();

			_repository.Save(user);
			return user;
		}

		public void Login(User user, bool stayLoggedIn)
		{
			user.UpdateLastActivity();
			_userAuthentication.Login(user.Username, stayLoggedIn);
		}


		public bool Login(string username, string password, bool stayLoggedIn)
		{
			var withMatchingNameOrEmail = new AdHoc<User>(x => x.Username == username || x.Email == username);
			var user = _repository.FindOne(withMatchingNameOrEmail);

			if (user != null)
			{
				//use the users salt and provided password to see if the password match
				string expetectedPassword = PasswordHelper.GetPasswordHash(password, user.Salt);
				if (user.Password == expetectedPassword)
				{
					Login(user, stayLoggedIn);
				}
			}

			return false;
		}


		public User GetUser(string username)
		{
			var adhoc = new AdHoc<User>(x => x.Username == username);
			return _repository.FindOne(adhoc);
		}

		public void SaveWebsite(User user, string siteUrl)
		{
			if (string.IsNullOrWhiteSpace(siteUrl)) throw new ArgumentNullException("siteUrl");
			if (!UrlValidation.IsValidUrl(siteUrl)) throw new RulesException("SiteUrl", "Invalid URL");
			string html = _htmlService.GetHtmlFromUrl(siteUrl);
			string feedUrl = _feedFinder.GetFeedUrl(html);

			if (string.IsNullOrWhiteSpace(feedUrl))
			{
				feedUrl = null;
			}

			user.UpdateWebsite(siteUrl, feedUrl);
		}

		public void Logout(string name)
		{
			var user = GetUser(name);
			
			if(user != null)
			{
				user.UpdateLastActivity();
			}

			_userAuthentication.Logout();
		}

		public void SaveProfileImage(User user, HttpPostedFileBase profileImage)
		{
			//be very safe with image streams
			var exceptions = new List<Exception>();

			ImageProcessResult result = null;

			try
			{
				var originalImageNames = new ImageFileNamesDto
											{
												ThumbFilename = user.ProfileImage.ThumbFileName,
												MediumFilename = user.ProfileImage.MediumFileName,
												FullFilename = user.ProfileImage.FullFileName
											};

				var newImageFileNames = new ImageFileNamesDto();

				result = _imageProcessor.ProcessImage(profileImage);
				bool fullFileExists = result.FullImage != null;

				var files = new List<FileToUpload>
				            	{
				            		new FileToUpload(result.ThumbnailImage, "thumb_" + profileImage.FileName),
				            		new FileToUpload(result.MediumImage, "med_" + profileImage.FileName)
				            	};

				if (fullFileExists)
				{
					files.Add(new FileToUpload(result.FullImage, "full_" + profileImage.FileName));
				}

				IList<string> filePaths = _fileUploadService.UploadFiles("Images/", files);

				newImageFileNames.ThumbFilename = filePaths[0];
				newImageFileNames.MediumFilename = filePaths[1];
				newImageFileNames.FullFilename = filePaths[fullFileExists ? 2 : 1];

				user.UpdateProfileImage(profileImage.FileName, newImageFileNames.ThumbFilename, newImageFileNames.MediumFilename, newImageFileNames.FullFilename);

				DeletePreviousProfileImages(originalImageNames);
			}
			catch (RulesException)
			{
				throw;
			}
			catch (ArgumentException arx)
			{
				throw new RulesException("Image", "Invalid file type");
				//TODO:alert the dev team somehow? could be malicious activity
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

		private void DeletePreviousProfileImages(ImageFileNamesDto originalImageNames)
		{
			var filesToDelete = new List<string>(3);
			if (!string.IsNullOrWhiteSpace(originalImageNames.ThumbFilename))
			{
				filesToDelete.Add(originalImageNames.ThumbFilename);
			}
			if (!string.IsNullOrWhiteSpace(originalImageNames.MediumFilename))
			{
				filesToDelete.Add(originalImageNames.MediumFilename);
			}
			if (!string.IsNullOrWhiteSpace(originalImageNames.FullFilename))
			{
				filesToDelete.Add(originalImageNames.FullFilename);
			}
			_fileUploadService.DeleteFiles("Images/", filesToDelete);
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
												UserDto = new UserDto { Username = user.Username, InterestDtos = user.Interests.Select(x => new UserInterestDto(x.Interest.Id, x.Interest.Name, x.Interest.Slug)).ToList(), Bio = user.Bio }
											}).ToList();
		}
	}
}