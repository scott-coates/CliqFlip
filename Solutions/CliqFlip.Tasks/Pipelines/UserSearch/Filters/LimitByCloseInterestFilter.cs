using System;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class LimitByCloseInterestFilter : ILimitByCloseInterestFilter
    {
        private readonly ICloseInterestLimiter _closeInterestLimiter;

        public LimitByCloseInterestFilter(ICloseInterestLimiter closeInterestLimiter)
        {
            _closeInterestLimiter = closeInterestLimiter;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "Scored results should be provided");

            pipelineResult.ScoredInterests = _closeInterestLimiter.LimitCloseInterests(pipelineResult.ScoredInterests);
        }
    }
}