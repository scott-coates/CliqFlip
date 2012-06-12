using System;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class SortUserScoreFilter : ISortUserScoreFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.Users == null) throw new ArgumentNullException("pipelineResult", "Users must be provided");

            pipelineResult.Users = pipelineResult
                .Users
                .OrderByDescending(x => x.Score)
                .ToList();
        }
    }
}