using CliqFlip.Infrastructure.Email.Interfaces;

namespace CliqFlip.Infrastructure.Email
{
	public class SESEmailService : IEmailService
	{
		public void SendMail(string to, string @from, string subject)
		{
			throw new System.NotImplementedException();
		}
	}
}