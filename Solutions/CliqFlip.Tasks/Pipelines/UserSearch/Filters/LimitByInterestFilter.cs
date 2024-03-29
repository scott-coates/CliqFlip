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
        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "ScoredInterests is required");

            var interestIds = pipelineResult
                .ScoredInterests
                .Select(x => x.Id)
                .ToList(); //this needs to be a list since it's used in a query expression

            pipelineResult.UserQuery =
                pipelineResult
                    .UserQuery
                    .Where(x => x.Interests.Any(y => interestIds.Contains(y.Interest.Id)));
        }
    }
}