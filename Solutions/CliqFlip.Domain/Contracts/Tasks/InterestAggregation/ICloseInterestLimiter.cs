using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Domain.Contracts.Tasks.InterestAggregation
{
    public interface ICloseInterestLimiter
    {
        IList<ScoredRelatedInterestDto> LimitCloseInterests(IList<ScoredRelatedInterestDto> scoredInterests);
    }
}