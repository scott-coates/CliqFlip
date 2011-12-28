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

		public UserTasks(ILinqRepository<User> repository)
		{
			_repository = repository;
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
	}
}