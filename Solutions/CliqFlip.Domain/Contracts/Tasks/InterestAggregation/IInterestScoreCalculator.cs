using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;
using CliqFlip.Domain.Dtos.UserInterest;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface IInterestScoreCalculator
    {
        void CalculateRelatedInterestScore<T>(IList<T> weightedInterests) where T : class, IWeightedInterestDto;
    }
}