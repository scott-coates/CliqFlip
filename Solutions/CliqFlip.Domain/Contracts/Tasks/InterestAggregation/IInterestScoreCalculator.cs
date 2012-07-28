using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface IInterestScoreCalculator
    {
        IList<ScoredRelatedInterestDto> CalculateRelatedInterestScore(IList<WeightedRelatedInterestDto> relatedInterests);
    }
}