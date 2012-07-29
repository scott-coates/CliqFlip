using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface IHighestScoreCalculator
    {
        IList<ScoredRelatedInterestDto> CalculateHighestScores(IList<ScoredRelatedInterestDto> scoredInterests);
    }
}