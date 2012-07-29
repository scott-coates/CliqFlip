using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class HighestScoreCalculator : IHighestScoreCalculator
    {
        public IList<ScoredRelatedInterestDto> CalculateHighestScores(IList<ScoredRelatedInterestDto> scoredInterests)
        {
            if (scoredInterests == null) throw new ArgumentNullException("scoredInterests");

            //http://stackoverflow.com/a/4874031/173957
            return scoredInterests
                .GroupBy(x => x.Id, (y, z) => z.Aggregate((a, x) => a.Score > x.Score ? a : x))
                .OrderByDescending(x => x.Score)
                .ToList();
        }
    }
}