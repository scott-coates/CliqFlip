using System;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class TakeHighestScoredInterestFilter : ITakeHighestScoredInterestFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "Scored results should be provided");

            //http://stackoverflow.com/a/4874031/173957
            var highestScoredInterests = pipelineResult.ScoredInterests
                .GroupBy(x => x.Id, (y, z) => z.Aggregate((a, x) => a.Score > x.Score ? a : x))
                .OrderByDescending(x => x.Score)
                .ToList();

            pipelineResult.ScoredInterests = highestScoredInterests;
        }
    }
}