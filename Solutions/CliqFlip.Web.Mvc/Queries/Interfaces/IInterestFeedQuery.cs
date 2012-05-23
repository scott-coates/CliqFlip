using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface IInterestFeedQuery
	{
		InterestsFeedViewModel GetUsersByInterests(string userName, int? page, UrlHelper url);
	}
}