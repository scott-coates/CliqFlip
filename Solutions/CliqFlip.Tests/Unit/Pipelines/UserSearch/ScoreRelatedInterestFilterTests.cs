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
    public class ScoreRelatedInterestFilterTests
    {
        private readonly IScoreRelatedInterestFilter _scoreRelatedInterestFilter = new ScoreRelatedInterestFilter();
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullInputReturnsError()
        {
            _scoreRelatedInterestFilter.Filter(null);
        }

        [Test]
        public void ExactMatchIsEqualToMaxHopsCounter()
        {
            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                RelatedInterests = new List<WeightedRelatedInterestDto>
                {
                    new WeightedRelatedInterestDto(0, new List<float>(), "")
                }
            };

            _scoreRelatedInterestFilter.Filter(userSearchPipelineResult);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(_maxHopsInverter));
        }

        [TestCase(new float[] { }, 4f)]
        [TestCase(new[] { .25f, .75f }, .75f)]
        [TestCase(new[] { .25f, .75f, .75f }, .5625f)]
        public void ScoreIsCalculatedCorrectly(float[] weights, float expectedScore)
        {
            var constructedRelatedInterest = new WeightedRelatedInterestDto(0, weights.ToList(), "");
            var userSearchPipelineResult = new UserSearchPipelineResult
            {
                RelatedInterests = new List<WeightedRelatedInterestDto>
                {
                    constructedRelatedInterest
                }
            };

            _scoreRelatedInterestFilter.Filter(userSearchPipelineResult);

            Assert.That(userSearchPipelineResult.ScoredInterests.Single().Score, Is.EqualTo(expectedScore));
        }
    }
}