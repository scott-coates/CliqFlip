using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface ICloseInterestLimiter
    {
        IList<IScoredInterestDto> LimitCloseInterests<T>(IList<T> scoredInterests) where T : class, IScoredInterestDto;
    }
}