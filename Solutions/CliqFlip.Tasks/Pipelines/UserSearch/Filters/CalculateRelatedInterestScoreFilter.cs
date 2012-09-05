using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Contracts.Tasks.InterestAggregation;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateRelatedInterestScoreFilter : ICalculateRelatedInterestScoreFilter
    {
        private readonly IInterestScoreCalculator _interestScoreCalculator;

        public CalculateRelatedInterestScoreFilter(IInterestScoreCalculator interestScoreCalculator)
        {
            _interestScoreCalculator = interestScoreCalculator;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");

            _interestScoreCalculator.CalculateRelatedInterestScore(pipelineResult.RelatedInterests);

            pipelineResult.ScoredInterests = pipelineResult.RelatedInterests.Select(x => new ScoredRelatedInterestDto(x.Id, x.Score, x.Slug, x.IsMainCategory, x.ExplicitSearch, x.Weight.Count)).ToList();
        }
    }
}