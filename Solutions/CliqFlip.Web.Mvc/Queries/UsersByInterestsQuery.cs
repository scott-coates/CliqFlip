using System.Collections.Generic;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UsersByInterestsQuery : IUsersByInterestsQuery
	{
		public UsersByInterestsSearchResultsViewModel GetGetUsersByInterests(IEnumerable<int> interestIds)
		{
			
		}
	}
}