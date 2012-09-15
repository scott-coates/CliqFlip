using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using Amazon;
using Amazon.SimpleEmail.Model;
using CliqFlip.Common;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Logging.Interfaces;

namespace CliqFlip.Infrastructure.Email
{
	public class SESEmailService : IEmailService
	{
		public void SendMail(string to, string subject, string body)
		{
			//smtp would've been quicker but the SSL that .NET uses isn't the same as AWS
			using(var sesClient  = AWSClientFactory.CreateAmazonSimpleEmailServiceClient() )
			{
				string sesFromEmail = ConfigurationManager.AppSettings[Constants.SES_FROM_EMAIL];

				var sendEmailRequest = new SendEmailRequest()
					.WithDestination(new Destination().WithToAddresses(to))
					.WithSource(sesFromEmail) // The sender's email address.
					.WithReturnPath(sesFromEmail)// The email address to which bounce notifications are to be forwarded.
					.WithMessage(new Message()
					             	.WithBody(new Body().WithHtml(new Content(body).WithCharset("UTF-8")))
					             	.WithSubject(new Content(subject).WithCharset("UTF-8")));

				var response = sesClient.SendEmail(sendEmailRequest);
			}
		}
	}
}