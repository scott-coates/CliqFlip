﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserSaveInterestImageViewModel
	{
		public int UserInterestId { get; set; }
		public string ImageDescription { get; set; }
		public HttpPostedFileBase InterestImage { get; set; }
	}
}