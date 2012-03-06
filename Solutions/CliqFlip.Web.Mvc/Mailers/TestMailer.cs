using System.Net.Mail;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using Mvc.Mailer;

namespace CliqFlip.Web.Mvc.Mailers
{
	public class TestMailer : MailerBase, ITestMailer
	{
		public TestMailer()
		{
			MasterName = "_Layout";
		}

		#region ITestMailer Members

		public virtual MailMessage TestSimpleSend(SimpleSendEmailViewModel simpleSendEmailViewModel)
		{
			var mailMessage = new MailMessage();
			
			ViewData.Model = simpleSendEmailViewModel;

			PopulateBody(mailMessage, viewName: "TestSimpleSend");
			
			return mailMessage;
		}

		#endregion
	}
}