using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface IHighestScoreCalculator
    {
        IList<IScoredInterestDto> CalculateHighestScores<T>(IList<T> scoredInterests) where T : class, IScoredInterestDto;
    }
}