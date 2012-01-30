﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CliqFlip.Web.Mvc.ViewModels.Search;
using CliqFlip.Web.Mvc.ViewModels.User;

namespace CliqFlip.Web.Mvc.Queries.Interfaces
{
	public interface IUserProfileQuery
	{
		UserProfileViewModel GetUser(string username);
	}
}