using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserAccountViewModel : UserProfileViewModel
	{
		public UserAccountEmailViewModel UserAccountEmail { get; set; }
		public UserAccountLocationViewModel UserAccountLocation { get; set; }
		public UserAccountPasswordViewModel UserAccountPassword { get; set; }

		public UserAccountViewModel()
		{
			UserAccountEmail = new UserAccountEmailViewModel();
			UserAccountLocation = new UserAccountLocationViewModel();
			UserAccountPassword = new UserAccountPasswordViewModel();
		}

		#region Nested type: UserAccountEmailViewModel

		public class UserAccountEmailViewModel
		{
			[Required(ErrorMessage = "Please provide an email address.")]
			[Display(Name = "What's your email?")]
			[EmailAddress(ErrorMessage = "Please provide a valid email address.")]
			[Remote("Email", "Validation", "Admin", ErrorMessage = "This email is taken... Are you sure you're not signed up already?")]
			public String Email { get; set; }
		}

		#endregion

		#region Nested type: UserAccountLocationViewModel

		public class UserAccountLocationViewModel
		{
			[Required(ErrorMessage = "Please provide your zip code")]
			[Display(Name = "Location (postal code or city & state):")]
			[Remote("Location", "Validation", "Admin")]
			public string Location { get; set; }
		}

		#endregion

		#region Nested type: UserAccountPasswordViewModel

		public class UserAccountPasswordViewModel
		{
			[Required(ErrorMessage = "Please provide a password.")]
			[Display(Name = "Set your password. Choose a strong one.")]
			[DataType(DataType.Password)]
			public String Password { get; set; }

			[Required(ErrorMessage = "Please type your password again.")]
			[Compare("Password", ErrorMessage = "Password do not match")]
			[Display(Name = "Please type your password again")]
			[DataType(DataType.Password)]
			public String PasswordVerify { get; set; }
		}

		#endregion
	}
}