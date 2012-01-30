using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Extensions.Validation;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserProfileViewModel
	{
		public string Username { get; set; } 
		public string InterestsJson { get; set; } 
	}
}