using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class CalculateRelatedInterestScoreFilter : ICalculateRelatedInterestScoreFilter
    {
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");

            var scoredRelatedInterestDtos = new List<ScoredRelatedInterestDto>();

            foreach (var ret in pipelineResult.RelatedInterests)
            {
                var score = (float)_maxHopsInverter;

                if (ret.Weight.Any())
                {
                    score *= ret.Weight.Aggregate((f, f1) => f * f1);
                }

                var scoredInterest = new ScoredRelatedInterestDto(ret.Id, score, ret.Slug, ret.IsMainCategory, ret.ExplicitSearch, ret.Weight.Count);
                scoredRelatedInterestDtos.Add(scoredInterest);
            }

            pipelineResult.ScoredInterests = scoredRelatedInterestDtos;
        }
    }
}