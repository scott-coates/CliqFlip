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
        private readonly IInterestTasks _interestTasks;

        public UserTasks(ILinqRepository<User> repository, IInterestTasks interestTasks)
		{
			_repository = repository;
            _interestTasks = interestTasks;
		}


		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IEnumerable<int> interestIds)
		{
			var interestList = interestIds.ToList();
			var query = new AdHoc<User>(x => x.Interests.Any(y => interestList.Contains(y.Id)));

			var users = _repository.FindAll(query).ToList();
			return users.Select(user => new UserSearchByInterestsDto
			                            	{
			                            		MatchCount = user.Interests.Select(x => x.Id).Intersect(interestList).Count(),
			                            		UserDto = new UserDto {Username = user.Username, InterestDtos = user.Interests.Select(x => new InterestDto(x.Id, x.Name)).ToList(), Bio = user.Bio}
			                            	}).ToList();
		}


        public UserDto Create(UserDto userToCreate)
        {
            User user = new User(userToCreate.Username, userToCreate.Email, userToCreate.Password);

            foreach (var userInterest in userToCreate.InterestDtos)
            {
                var interest = _interestTasks.GetOrCreate(userInterest.Name);
                user.Interests.Add(new Interest(interest.Id, interest.Name));
            }
            _repository.Save(user);
            return new UserDto { Username = user.Username, Email = user.Email, Password = user.Password };
        }
    }
}