using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Tasks.TaskImpl;
using Moq;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks
{
	[TestFixture]
	public class UserTaskTests
	{
		private IUserTasks _userTasks;
		private IHtmlService _htmlService;
		private IFeedFinder _feedFinder;
		private User _user;

		[SetUp]
		public void Setup()
		{
			_htmlService = new Mock<IHtmlService>().Object;
			_feedFinder = new Mock<IFeedFinder>().Object;
			_user = new Mock<User> { CallBase = true }.Object;
			_userTasks = new UserTasks(null, null, null, null, _htmlService, _feedFinder, null, null);
		}

		#region Website tests

		[TestCase("http://somewebsite.com/")]
		[TestCase("www.somewebsite.com/", ExpectedException = typeof(RulesException))]
		[TestCase("fake site", ExpectedException = typeof(RulesException))]
		[TestCase("", ExpectedException = typeof(ArgumentNullException))]
		public void InvalidUrlNotAccepted(string url)
		{
			_userTasks.SaveWebsite(_user, url);
			Assert.Pass("Website Url");
		}

		#endregion

	}
}
