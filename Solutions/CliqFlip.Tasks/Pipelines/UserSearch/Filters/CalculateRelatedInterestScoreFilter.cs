using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateRelatedInterestScoreFilter : ICalculateRelatedInterestScoreFilter
    {
        private readonly IInterestTasks _interestTasks;

        public CalculateRelatedInterestScoreFilter(IInterestTasks interestTasks)
        {
            _interestTasks = interestTasks;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");

            pipelineResult.ScoredInterests = _interestTasks.CalculateRelatedInterestScore(pipelineResult.RelatedInterests);
        }
    }
}