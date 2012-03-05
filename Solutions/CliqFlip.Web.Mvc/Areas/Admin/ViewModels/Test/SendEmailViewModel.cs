using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test
{
	public class SendEmailViewModel
	{
		[Required(ErrorMessage = "You forgot the 'to' email address, bro.")]
		[EmailAddress(ErrorMessage = "Not a valid email address, bro. C'mon!")]
		[Display(Name = "To Email Address:")]
		public string ToEmailAddress { get; set; }

		[Required(ErrorMessage = "We need a subject")]
		[Display(Name = "Subject")]
		public String Subject { get; set; }

		[Required(ErrorMessage = "We need a body")]
		[Display(Name = "Body")]
		[DataType(DataType.MultilineText)]
		public String Body { get; set; }
	}
}