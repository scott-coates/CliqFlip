using System;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Tasks.Pipelines.UserSearch.Filters;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Pipelines.UserSearch
{
	[TestFixture]
    public class ScoreRelatedInterestFilterTests
	{
	    private readonly IScoreRelatedInterestFilter _scoreRelatedInterestFilter = new ScoreRelatedInterestFilter();

		[SetUp]
		public void Setup()
		{
		    
		}

		
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
		public void NullInputReturnsError()
		{
		    _scoreRelatedInterestFilter.Filter(null);		
        }

        //[TestCase("http://somewebsite.com/")]
        //[TestCase("www.somewebsite.com/", ExpectedException = typeof(RulesException))]
        //[TestCase("fake site", ExpectedException = typeof(RulesException))]
        //[TestCase("", ExpectedException = typeof(ArgumentNullException))]
        //public void InvalidUrlNotAccepted(string url)
        //{
        //    _userTasks.SaveWebsite(_user, url);
        //    Assert.Pass("Website Url");
        //}
	}
}
