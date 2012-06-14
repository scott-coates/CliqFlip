using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateExplicitSearchInterestScoreFilter : ICalculateExplicitSearchInterestScoreFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "Scored results should be provided");

            var scoredRelatedInterestDtos = pipelineResult
                .ScoredInterests
                .Where(x => x.ExplicitSearch && x.Hops < Constants.EXPLICIT_SEARCH_INTEREST_MULTIPLIER.Length)
                .ToList();

            foreach (var scoredRelatedInterestDto in scoredRelatedInterestDtos)
            {
                scoredRelatedInterestDto.Score *= Constants.EXPLICIT_SEARCH_INTEREST_MULTIPLIER[scoredRelatedInterestDto.Hops];
            }
        }
    }
}