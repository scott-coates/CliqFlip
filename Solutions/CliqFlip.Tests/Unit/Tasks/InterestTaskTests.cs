using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Tasks.Tasks.Entities;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks
{
	[TestFixture]
	public class InterestTaskTests
	{
	    private IInterestTasks _interestTasks;
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

		[SetUp]
		public void Setup()
		{
		    _interestTasks = new InterestTasks(null, null);
		}


        [Test]
        public void ExactMatchIsEqualToMaxHopsCounter()
        {
            var relatedInterests = new List<WeightedRelatedInterestDto>
            {
                new WeightedRelatedInterestDto(0, new List<float>(), "", false)
            };

            var result = _interestTasks.CalculateRelatedInterestScore(relatedInterests);

            Assert.That(result.Single().Score, Is.EqualTo(_maxHopsInverter));
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

            var result = _interestTasks.CalculateRelatedInterestScore(relatedInterests);

            Assert.That(result.Single().Score, Is.EqualTo(expectedScore));
        }
	}
}
