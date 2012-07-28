using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class InterestScoreCalculator : IInterestScoreCalculator
    {
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        public IList<ScoredRelatedInterestDto> CalculateRelatedInterestScore(IList<WeightedRelatedInterestDto> relatedInterests)
        {
            var retVal = new List<ScoredRelatedInterestDto>();

            foreach (var ret in relatedInterests)
            {
                var score = (float)_maxHopsInverter;

                if (ret.Weight.Any())
                {
                    score *= ret.Weight.Aggregate((f, f1) => f * f1);
                }

                var scoredInterest = new ScoredRelatedInterestDto(ret.Id, score, ret.Slug, ret.IsMainCategory, ret.ExplicitSearch, ret.Weight.Count);

                retVal.Add(scoredInterest);
            }

            return retVal;
        }
    }
}