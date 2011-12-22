using System.Collections.Generic;
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

		public UsersByInterestsSearchResultsViewModel GetGetUsersByInterests(IEnumerable<int> interestIds)
		{
			var users = _userTasks.GetUsersByInterestsDtos(interestIds);
			var retVal = new UsersByInterestsSearchResultsViewModel {UserDtos = users};
			return retVal;
		}
	}
}