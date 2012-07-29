using System;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateHighestScoredInterestFilter : ICalculateHighestScoredInterestFilter
    {
        private readonly IHighestScoreCalculator _highestScoreCalculator;

        public CalculateHighestScoredInterestFilter(IHighestScoreCalculator highestScoreCalculator)
        {
            _highestScoreCalculator = highestScoreCalculator;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (pipelineResult.ScoredInterests == null) throw new ArgumentNullException("pipelineResult", "Scored results should be provided");

            var highestScoredInterests = _highestScoreCalculator.CalculateHighestScores(pipelineResult.ScoredInterests);

            pipelineResult.ScoredInterests = highestScoredInterests;
        }
    }
}