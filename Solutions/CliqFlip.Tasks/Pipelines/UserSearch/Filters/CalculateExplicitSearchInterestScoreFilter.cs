using System;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateExplicitSearchInterestScoreFilter : ICalculateExplicitSearchInterestScoreFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult","Scored results should be provided");

            foreach (var scoredRelatedInterestDto in pipelineResult.ScoredInterests.Where(x=>x.ExplicitSearch))
            {
                scoredRelatedInterestDto.Score *= Constants.EXPLICIT_SEARCH_INTEREST_MULTIPLIER;
            }

            var belowThreshold = pipelineResult.ScoredInterests.Where(x => x.Score < Constants.EXPLICIT_SEARCH_INTEREST_THRESHOLD).ToList();
            belowThreshold.ForEach(x => pipelineResult.ScoredInterests.Remove(x));
        }
    }
}