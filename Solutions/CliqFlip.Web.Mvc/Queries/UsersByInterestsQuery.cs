using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UsersByInterestsQuery : IUsersByInterestsQuery
	{
		private readonly IUserTasks _userTasks;

		public UsersByInterestsQuery(IUserTasks userTasks)
		{
			_userTasks = userTasks;
		}

		public UsersByInterestViewModel GetGetUsersByInterests(IEnumerable<int> interestIds)
		{
			var users = _userTasks.GetUsersByInterestsDtos(interestIds);
			var retVal = new UsersByInterestViewModel();
			foreach (var user in users)
			{
				string interests = string.Join(", ", user.UserDto.InterestDtos.Select(x => x.Name));
				retVal.Results.Add(new IndividualResultViewModel {Name = user.UserDto.Username, Interests = interests, Bio = user.UserDto.Bio});
			}
			return retVal;
		}
	}
}