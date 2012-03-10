using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.ViewModels.Search;
using CliqFlip.Web.Mvc.ViewModels.User;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface IUserProfileQuery
	{
		UserProfileIndexViewModel GetUserProfileIndex(string username, IPrincipal requestingUser);
		UserSocialMediaViewModel GetUserSocialMedia(string username, IPrincipal requestingUser);
		UserInterestsViewModel GetUserIntersets(string username, IPrincipal requestingUser);
        UserInboxViewModel GetUsersInbox(IPrincipal requestingUser);
	}
}