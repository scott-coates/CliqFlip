using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class CloseInterestLimiter : ICloseInterestLimiter
    {
        public IList<ScoredRelatedInterestDto> LimitCloseInterests(IList<ScoredRelatedInterestDto> scoredInterests)
        {
            return scoredInterests.Where(x => x.Score >= Constants.CLOSE_INTEREST_THRESHOLD).ToList();
        }
    }
}