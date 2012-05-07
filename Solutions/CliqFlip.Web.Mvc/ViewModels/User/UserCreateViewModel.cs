using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.Validation;
using Microsoft.Web.Mvc;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserCreateViewModel
	{
		public UserCreateViewModel()
		{
			UserInterests = new List<InterestCreate>();
		}

		[Required(ErrorMessage = "Please choose a username")]
		[Display(Name = "Choose a username:")]
        [Remote("Username", "Validation", "Admin", ErrorMessage = "This username is taken... Try another one.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Please provide your zip code")]
		[Display(Name = "Zip Code:")]
		public string ZipCode { get; set; }

		[Required(ErrorMessage = "Please provide an email address.")]
		[Display(Name = "Whats your email?")]
		[EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        [Remote("Email", "Validation", "Admin", ErrorMessage = "This email is taken... Are you sure you're not signed up already?")]
        public String Email { get; set; }

		[Required(ErrorMessage = "Please provide a password.")]
		[Display(Name = "Set your password. Choose a strong one.")]
		[DataType(DataType.Password)]
		public String Password { get; set; }

		[Required(ErrorMessage = "Please type your password again.")]
		[Compare("Password", ErrorMessage = "Password do not match")]
		[Display(Name = "Please type your password again")]
		[DataType(DataType.Password)]
		public String PasswordVerify { get; set; }

		[MustBeTrue(ErrorMessage = "You must accept the terms and conditions")]
		[Display(Name = "Accept terms and conditions")]
		public bool AcceptTermsAndConditions { get; set; }

		[CollectionNotEmptyAttribute(ErrorMessage = "Okay, we get it, you're not very interesting. But please, for our sake, just provide us with an interest that tells us about yourself.")]
		public List<InterestCreate> UserInterests { get; set; }

	}
}