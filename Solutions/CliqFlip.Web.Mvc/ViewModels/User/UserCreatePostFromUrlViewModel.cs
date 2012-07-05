using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserCreatePostFromUrlViewModel
	{
		public int UserInterestId { get; set; }
		public string PostDescription { get; set; }
		[Required]
		public string MediumUrl { get; set; }
	}
}