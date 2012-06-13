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
    public class CalculatedHighestScoredInterestFilterTests
    {
        private readonly ICalculatedHighestScoredInterestFilter _calculatedHighestScoredInterestFilter = new CalculatedHighestScoredInterestFilter();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HighestInGroupIsReturned()
        {
            var score1 = new ScoredRelatedInterestDto(1, 4f, "", false, false);
            var score2 = new ScoredRelatedInterestDto(1, 16f, "", false, false);

            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                ScoredInterests = new List<ScoredRelatedInterestDto>
                {
                    score1,
                    score2
                }
            };

            _calculatedHighestScoredInterestFilter.Filter(userSearchPipelineResult, null);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single(), Is.EqualTo(score2));
        }
    }
}