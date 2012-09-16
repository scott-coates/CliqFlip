using System;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Email;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using Moq;
using NUnit.Framework;

namespace CliqFlip.Tests.Integration.Email
{
	[TestFixture]
    [Ignore]
    public class SESEmailTests
	{
		private IEmailService _emailService;

		[SetUp]
		public void Setup()
		{
			_emailService = new SESEmailService();
		}

		#region Email tests

		[Ignore]
		[Test]
		public void SendEmail()
		{
			Assert.Pass("Send email");
		}

		#endregion

	}
}
