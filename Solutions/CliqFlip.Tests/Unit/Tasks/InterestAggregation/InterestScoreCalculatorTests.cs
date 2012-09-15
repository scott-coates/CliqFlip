using System.Collections.Generic;
using System.Linq;
using CliqFlip.Common;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Tasks.Tasks.Entities;
using CliqFlip.Tasks.Tasks.InterestAggregation;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks.InterestAggregation
{
	[TestFixture]
	public class InterestScoreCalculatorTests
	{
	    private IInterestScoreCalculator _interestScoreCalculator;
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

		[SetUp]
		public void Setup()
		{
		    _interestScoreCalculator = new InterestScoreCalculator();
		}


        [Test]
        public void ExactMatchIsEqualToMaxHopsCounter()
        {
            var relatedInterests = new List<WeightedRelatedInterestDto>
            {
                new WeightedRelatedInterestDto(0, new List<float>(), "", false)
            };

            _interestScoreCalculator.CalculateRelatedInterestScore(relatedInterests);

            Assert.That(relatedInterests.Single().Score, Is.EqualTo(_maxHopsInverter));
        }

        [TestCase(new float[] { }, 4f)]
        [TestCase(new[] { .25f, .75f }, .75f)]
        [TestCase(new[] { .25f, .75f, .75f }, .5625f)]
        public void ScoreIsCalculatedCorrectly(float[] weights, float expectedScore)
        {
            var constructedRelatedInterest = new WeightedRelatedInterestDto(0, weights.ToList(), "", false);
            var relatedInterests = new List<WeightedRelatedInterestDto>
            {
                constructedRelatedInterest
            };

            _interestScoreCalculator.CalculateRelatedInterestScore(relatedInterests);

            Assert.That(relatedInterests.Single().Score, Is.EqualTo(expectedScore));
        }
	}
}
