using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;
using CliqFlip.Infrastructure;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly ILinqRepository<User> _repository;
		private readonly IInterestTasks _interestTasks;

		public UserTasks(ILinqRepository<User> repository, IInterestTasks interestTasks)
		{
			_repository = repository;
			_interestTasks = interestTasks;
		}

		#region IUserTasks Members

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases)
		{
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
																	.Select(x => new InterestDto(x.Interest.Id, x.Interest.Name, x.Interest.Slug)).ToList(),
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
				return null;
			}


            var salt = PasswordHelper.GenerateSalt(8);
            var pHash = PasswordHelper.GetPasswordHash(userToCreate.Password, salt);

			var user = new User(userToCreate.Username, userToCreate.Email, pHash, salt);

			//add all the interests
			foreach (InterestDto userInterest in userToCreate.InterestDtos)
			{
				//get or create the Interest
				InterestDto interestDto = _interestTasks.GetOrCreate(userInterest.Name);

				var interest = new UserInterest
								{
									Interest = new Interest(interestDto.Id, interestDto.Name),
									SocialityPoints = userInterest.Sociality
								};

				user.Interests.Add(interest);
			}
			_repository.Save(user);
			return new UserDto { Username = user.Username, Email = user.Email, Password = user.Password };
		}

		#endregion

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IEnumerable<int> interestIds)
		{
			List<int> interestList = interestIds.ToList();
			var query = new AdHoc<User>(x => x.Interests.Any(y => interestList.Contains(y.Id)));

			List<User> users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
											{
												MatchCount = user.Interests.Select(x => x.Id).Intersect(interestList).Count(),
												UserDto = new UserDto { Username = user.Username, InterestDtos = user.Interests.Select(x => new InterestDto(x.Interest.Id, x.Interest.Name, x.Interest.Slug)).ToList(), Bio = user.Bio }
											}).ToList();
		}
	}
}