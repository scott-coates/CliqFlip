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
    public class ScoreExplicitSearchInterestFilterTests
    {
        private readonly IScoreExplicitSearchInterestFilter _scoreExplicitSearchInterestFilter = new ScoreExplicitSearchInterestFilter();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExplicitSearchIncreasesScore()
        {
            var score1 = new ScoredRelatedInterestDto(0, 4, "") { ExplicitSearch = true };

            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                ScoredInterests = new List<ScoredRelatedInterestDto>
                {
                    score1
                }
            };

            _scoreExplicitSearchInterestFilter.Filter(userSearchPipelineResult);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(16));
        }
    }
}