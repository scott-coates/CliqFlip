using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Logging.Interfaces;

namespace CliqFlip.Infrastructure.Email
{
	public class SESEmailService : IEmailService
	{
		private readonly ILogger _logger;

		private static readonly SmtpSection _smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");


		public SESEmailService(ILogger logger)
		{
			_logger = logger;
		}

		public void SendMail(string to, string subject, string body)
		{
			using (var client = new SmtpClient())
			{
				using (var message = new MailMessage(_smtpSection.From, to, subject, body))
				{
					message.BodyEncoding = Encoding.UTF8;
					message.IsBodyHtml = true;
					client.SendCompleted += ClientSendCompleted;
					client.SendAsync(message, null);
				}
			}
		}

		void ClientSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				_logger.LogException(e.Error);
			}
		}
	}
}