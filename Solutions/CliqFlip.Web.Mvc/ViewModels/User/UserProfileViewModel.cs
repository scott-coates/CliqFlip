using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserProfileViewModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Headline { get; set; }
		public string ProfileImageUrl { get; set; }
		public string FullProfileImageUrl { get; set; }
		public bool AuthenticatedProfileOwner { get; set; }
		public bool AuthenticatedVisitor { get; set; }
    }
}