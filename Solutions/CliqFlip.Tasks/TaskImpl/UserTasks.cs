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


		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IEnumerable<int> interestIds)
		{
			var interestList = interestIds.ToList();
			var query = new AdHoc<User>(x => x.Interests.Any(y => interestList.Contains(y.Id)));

			var users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
			                            	{
			                            		MatchCount = user.Interests.Select(x => x.Id).Intersect(interestList).Count(),
			                            		UserDto = new UserDto {Username = user.Username, InterestDtos = user.Interests.Select(x => new InterestDto(x.Subject.Id, x.Subject.Name)).ToList(), Bio = user.Bio}
			                            	}).ToList();
		}


        public UserDto Create(UserDto userToCreate)
        {
            var matchingNameOrEmail = new AdHoc<User>(x => x.Username == userToCreate.Username || x.Email == userToCreate.Email);
            //Check username and email are unique
            if (_repository.FindAll(matchingNameOrEmail).Any())
            {
                return null;
            }
            //TODO: Encrypt password
            User user = new User(userToCreate.Username, userToCreate.Email, userToCreate.Password);

            //add all the interests
            foreach (var userInterest in userToCreate.InterestDtos)
            {
                //get or create the subject
                var subject = _subjectTasks.SaveOrUpdate(userInterest.Name);

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
    }
}