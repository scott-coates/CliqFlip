using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class InterestScoreCalculator : IInterestScoreCalculator
    {
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        public void CalculateRelatedInterestScore<T>(IList<T> weightedInterests) where T : class, IWeightedInterestDto
        {
            foreach (var ret in weightedInterests)
            {
                var score = (float)_maxHopsInverter;

                if (ret.Weight.Any())
                {
                    score *= ret.Weight.Aggregate((f, f1) => f * f1);
                }

                ret.Score = score;
            }
        }
    }
}