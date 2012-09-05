using System;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Tasks.Tasks.Entities;
using Moq;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks.Entities
{
    [TestFixture]
    public class UserTaskTests
    {
        private IUserTasks _userTasks;
        private IWebContentService _webContentService;
        private IFeedFinder _feedFinder;
        private User _user;

        [SetUp]
        public void Setup()
        {
            _webContentService = new Mock<IWebContentService>().Object;
            _feedFinder = new Mock<IFeedFinder>().Object;
            _user = new Mock<User> { CallBase = true }.Object;
            _userTasks = new UserTasks(null, null, null, _webContentService, _feedFinder, null, null, null, null, null, null, null);
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
