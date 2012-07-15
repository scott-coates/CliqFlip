using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface IFeedPostOverviewQuery
	{
		FeedPostOverviewViewModel GetFeedPostOverview(int postId, User viewingUser);
	}
}