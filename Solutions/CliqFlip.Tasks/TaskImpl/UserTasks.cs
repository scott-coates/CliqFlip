using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly ILinqRepository<User> _repository;
		private readonly ISubjectTasks _subjectTasks;

		public UserTasks(ILinqRepository<User> repository, ISubjectTasks subjectTasks)
		{
			_repository = repository;
			_subjectTasks = subjectTasks;
		}

		#region IUserTasks Members

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> subjectAliases)
		{
			IList<string> subjAliasAndParent = _subjectTasks.GetSystemAliasAndParentAlias(subjectAliases);
			var query = new AdHoc<User>(x => x.Interests.Any(y => subjAliasAndParent.Contains(y.Subject.SystemAlias))
											 ||
											 x.Interests.Any(y => subjAliasAndParent.Contains(y.Subject.ParentSubject.SystemAlias)));

			List<User> users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
											{
												MatchCount = user.Interests.Sum(x =>
																					{
																						if (subjectAliases.Contains(x.Subject.SystemAlias))
																							return 3; //movies -> movies (same match)
																						if (x.Subject.ParentSubject != null && subjAliasAndParent.Contains(x.Subject.ParentSubject.SystemAlias))
																							return 2; //movies -> tv shows (sibling match)
																						if (subjAliasAndParent.Contains(x.Subject.SystemAlias))
																							return 1; //movies -> entertainment (parent match)
																						return 0;
																					}),
												UserDto = new UserDto
															{
																Username = user.Username,
																InterestDtos = user.Interests
																	.Select(x => new InterestDto(x.Subject.Id, x.Subject.Name, x.Subject.SystemAlias)).ToList(),
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

			//TODO: Encrypt password
			var user = new User(userToCreate.Username, userToCreate.Email, userToCreate.Password);

			//add all the interests
			foreach (InterestDto userInterest in userToCreate.InterestDtos)
			{
				//get or create the subject
				InterestDto subject = _subjectTasks.GetOrCreate(userInterest.Name);

				var interest = new Interest
								{
									Subject = new Subject(subject.Id, subject.Name),
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
												UserDto = new UserDto { Username = user.Username, InterestDtos = user.Interests.Select(x => new InterestDto(x.Subject.Id, x.Subject.Name, x.Subject.SystemAlias)).ToList(), Bio = user.Bio }
											}).ToList();
		}
	}
}