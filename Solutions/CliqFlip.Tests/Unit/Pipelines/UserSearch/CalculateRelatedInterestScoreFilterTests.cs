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
    public class CalculateRelatedInterestScoreFilterTests
    {
        private readonly ICalculateRelatedInterestScoreFilter _calculateRelatedInterestScoreFilter = new CalculateRelatedInterestScoreFilter();
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInputReturnsError()
        {
            _calculateRelatedInterestScoreFilter.Filter(null, null);
        }

        [Test]
        public void ExactMatchIsEqualToMaxHopsCounter()
        {
            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                RelatedInterests = new List<WeightedRelatedInterestDto>
                {
                    new WeightedRelatedInterestDto(0, new List<float>(), "", false)
                }
            };

            _calculateRelatedInterestScoreFilter.Filter(userSearchPipelineResult, null);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(_maxHopsInverter));
        }

        [TestCase(new float[] { }, 4f)]
        [TestCase(new[] { .25f, .75f }, .75f)]
        [TestCase(new[] { .25f, .75f, .75f }, .5625f)]
        public void ScoreIsCalculatedCorrectly(float[] weights, float expectedScore)
        {
            var constructedRelatedInterest = new WeightedRelatedInterestDto(0, weights.ToList(), "", false);
            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                RelatedInterests = new List<WeightedRelatedInterestDto>
                {
                    constructedRelatedInterest
                }
            };

            _calculateRelatedInterestScoreFilter.Filter(userSearchPipelineResult, null);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(expectedScore));
        }
    }
}