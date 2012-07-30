using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface IMainCategoryScoreCalculator
    {
        void CalculateMainCategoryScores(IList<ScoredRelatedInterestDto> scoredInterests);
    }
}