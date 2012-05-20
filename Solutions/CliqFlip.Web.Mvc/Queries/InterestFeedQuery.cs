using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
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

		public InterestsFeedViewModel GetGetUsersByInterests(string userName, int? page)
		{
			var retVal = new InterestsFeedViewModel();

			User user = _userTasks.GetUser(userName);

			IList<MediaSearchByInterestsDto> mediumDtos = _userInterestTasks.GetMediaByInterests(user.Interests.Select(x => x.Interest).ToList());
			retVal.Total = mediumDtos.Count;
			retVal.InterestViewModels = mediumDtos
				.AsPagination( page ?? 1, Constants.FEED_LIMIT)
				.Select(x => new InterestsFeedViewModel.InterestViewModel
				{
					Description = x.Description
				}).ToList();

			//rank them in order then grab that page
			return retVal;
		}

		#endregion
	}
}