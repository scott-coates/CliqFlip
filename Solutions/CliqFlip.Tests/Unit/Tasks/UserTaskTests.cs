using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Tasks.TaskImpl;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks
{
	[TestFixture]
	public class UserTaskTests
	{
		private IUserTasks _userTasks;

		[SetUp]
		public void Setup()
		{
			_userTasks = new UserTasks(null, null, null, null);
		}

		#region Website tests

		[TestCase("http://somewebsite.com/")]
		[TestCase("www.somewebsite.com/", ExpectedException = typeof(RulesException))]
		[TestCase("fake site", ExpectedException = typeof(RulesException))]
		[TestCase("", ExpectedException = typeof(ArgumentNullException))]
		public void InvalidUrlNotAccepted(string url)
		{
			_userTasks.SaveWebsite(null, url);
			Assert.Pass("Website Url");
		}

		#endregion

	}
}
