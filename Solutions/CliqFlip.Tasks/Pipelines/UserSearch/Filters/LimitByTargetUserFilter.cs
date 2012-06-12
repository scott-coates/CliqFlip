using System;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class LimitByTargetUserFilter : ILimitByTargetUserFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (request.User == null) throw new ArgumentNullException("request", "User cannot be null");

            pipelineResult.UserQuery =
                pipelineResult
                    .UserQuery
                    .Where(x => x != request.User);
        }
    }
}