using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.Queries
{
	public class InterestFeedQuery : IInterestFeedQuery
	{
		private readonly IUserInterestTasks _userInterestTasks;
		private readonly IUserTasks _userTasks;

		public InterestFeedQuery(IUserInterestTasks userInterestTasks, IUserTasks userTasks)
		{
			_userInterestTasks = userInterestTasks;
			_userTasks = userTasks;
		}

		#region IInterestFeedQuery Members

		public InterestsFeedViewModel GetUsersByInterests(string userName, int? page, UrlHelper url)
		{
			var retVal = new InterestsFeedViewModel();

			User user = _userTasks.GetUser(userName);

			IList<InterestFeedItemDto> postDtos = _userInterestTasks.GetPostsByInterests(user.Interests.Select(x => x.Interest).ToList());
			retVal.Total = postDtos.Count;
			retVal.InterestViewModels = postDtos
				.AsPagination( page ?? 1, Constants.FEED_LIMIT)
				.Select(x => new InterestsFeedViewModel.FeedPostViewModel(x) { UserPageUrl = url.Action("Index", "User", new { username = x.Username }) })
				.ToList();

			//rank them in order then grab that page
			return retVal;
		}

		#endregion
	}
}