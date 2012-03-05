using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

namespace CliqFlip.Web.Mvc.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		public UserMailer():
			base()
		{
			MasterName="_Layout";
		}

		
		public virtual MailMessage MessageNotification()
		{
			var mailMessage = new MailMessage{Subject = "MessageNotification"};
			
			//mailMessage.To.Add("some-email@example.com");
			//ViewBag.Data = someObject;
			PopulateBody(mailMessage, viewName: "MessageNotification");

			return mailMessage;
		}

		
	}
}