using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using Mvc.Mailer;
using System.Net.Mail;

namespace CliqFlip.Web.Mvc.Mailers
{ 
    public interface ITestMailer
    {
				
		MailMessage TestSimpleSend(SimpleSendEmailViewModel simpleSendEmailViewModel);
		
		
	}
}