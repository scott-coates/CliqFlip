using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks;
using SharpArch.Domain;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class LimitByInterestFilter : ILimitByInterestFilter
    {
        public UserSearchPipelineResult Filter(UserSearchPipelineResult pipelineResult)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "Interests is required");

            var interestIds = pipelineResult.ScoredInterests.Select(x => x.Id);

            pipelineResult.UserQuery =
                pipelineResult
                    .UserQuery
                    .Where(x => x.Interests.Any(y => interestIds.Contains(y.Interest.Id)));

            return pipelineResult;
        }
    }
}