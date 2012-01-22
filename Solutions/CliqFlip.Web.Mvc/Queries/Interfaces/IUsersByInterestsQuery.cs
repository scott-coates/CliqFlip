using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface IUsersByInterestsQuery
	{
		UsersByInterestViewModel GetGetUsersByInterests(string slugs, int? page);
	}
}