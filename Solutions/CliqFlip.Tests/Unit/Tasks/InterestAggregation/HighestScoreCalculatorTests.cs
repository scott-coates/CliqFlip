using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Tasks.Pipelines.UserSearch.Filters;
using CliqFlip.Tasks.Tasks.InterestAggregation;
using NUnit.Framework;

namespace CliqFlip.Tests.Unit.Tasks.InterestAggregation
{
    [TestFixture]
    public class HighestScoreCalculatorTests
    {
        private readonly IHighestScoreCalculator _highestScoreCalculator = new HighestScoreCalculator();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HighestInGroupIsReturned()
        {
            var score1 = new ScoredRelatedInterestDto(1, 4f, "", false, false, 0);
            var score2 = new ScoredRelatedInterestDto(1, 16f, "", false, false, 0);

            var scoredInterests = new List<ScoredRelatedInterestDto>
            {
                score1,
                score2
            };

            var returnedInterests = _highestScoreCalculator.CalculateHighestScores(scoredInterests);

            Assert.That(returnedInterests.Single(), Is.EqualTo(score2));
        }
    }
}