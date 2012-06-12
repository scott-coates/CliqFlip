using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Tasks.Pipelines.UserSearch.Filters;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Pipelines.UserSearch
{
    [TestFixture]
    public class TakeHighestScoredInterestFilterTests
    {
        private readonly ITakeHighestScoredInterestFilter _takeHighestScoredInterestFilter = new TakeHighestScoredInterestFilter();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HighestInGroupIsReturned()
        {
            var score1 = new ScoredRelatedInterestDto(1, 4f, "");
            var score2 = new ScoredRelatedInterestDto(1, 16f, "");

            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                ScoredInterests = new List<ScoredRelatedInterestDto>
                {
                    score1,
                    score2
                }
            };

            _takeHighestScoredInterestFilter.Filter(userSearchPipelineResult);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single(), Is.EqualTo(score2));
        }
    }
}