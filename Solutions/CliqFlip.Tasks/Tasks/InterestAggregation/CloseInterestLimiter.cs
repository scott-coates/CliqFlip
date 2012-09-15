using System.Collections.Generic;
using System.Linq;
using CliqFlip.Common;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Interest.Interfaces;

namespace CliqFlip.Tasks.Tasks.InterestAggregation
{
    public class CloseInterestLimiter : ICloseInterestLimiter
    {
        public IList<IScoredInterestDto> LimitCloseInterests<T>(IList<T> scoredInterests) where T : class, IScoredInterestDto
        {
            return scoredInterests.Where(x => x.Score >= Constants.CLOSE_INTEREST_THRESHOLD).ToList<IScoredInterestDto>();
        }
    }
}