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
    public class ScoreRelatedInterestFilter : IScoreRelatedInterestFilter
    {
        private static readonly int _maxHopsInverter = int.Parse(Constants.INTEREST_MAX_HOPS) + 1;

        public UserSearchPipelineResult Filter(UserSearchPipelineResult pipelineResult)
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
                var scoredInterest = new ScoredRelatedInterestDto(ret.Id, score, ret.Slug);
                scoredRelatedInterestDtos.Add(scoredInterest);
            }

            //http://stackoverflow.com/a/4874031/173957
            var highestScoredInterests = scoredRelatedInterestDtos
                .GroupBy(x => x.Id, (y, z) => z.Aggregate((a, x) => a.Score > x.Score ? a : x))
                .OrderByDescending(x => x.Score)
                .ToList();

            pipelineResult.ScoredInterests = highestScoredInterests;

            return pipelineResult;
        }
    }
}