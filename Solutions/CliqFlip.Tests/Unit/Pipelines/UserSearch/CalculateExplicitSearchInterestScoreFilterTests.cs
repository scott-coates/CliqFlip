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
    public class CalculateExplicitSearchInterestScoreFilterTests
    {
        private readonly ICalculateExplicitSearchInterestScoreFilter _calculateExplicitSearchInterestScoreFilter = new CalculateExplicitSearchInterestScoreFilter();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExplicitSearchIncreasesScore()
        {
            var score1 = new ScoredRelatedInterestDto(0, 4, "", false) { ExplicitSearch = true };

            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                ScoredInterests = new List<ScoredRelatedInterestDto>
                {
                    score1
                }
            };

            _calculateExplicitSearchInterestScoreFilter.Filter(userSearchPipelineResult, null);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(16));
        }
    }
}