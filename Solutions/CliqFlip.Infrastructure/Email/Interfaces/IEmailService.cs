using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.Email.Interfaces
{
	public interface IEmailService
	{
		void SendMail(string to, string subject, string body);
	}
}
