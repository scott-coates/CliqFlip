using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

namespace CliqFlip.Web.Mvc.Mailers
{ 
    public interface IUserMailer
    {
				
		MailMessage MessageNotification();
		
		
	}
}