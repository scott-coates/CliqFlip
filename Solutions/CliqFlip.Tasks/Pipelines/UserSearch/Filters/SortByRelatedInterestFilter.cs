using System;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class SortByRelatedInterestFilter : ISortByRelatedInterestFilter
    {
        public void Filter(UserSearchPipelineResult pipelineResult)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");

            foreach (var user in pipelineResult.Users)
            {
                user.Score += user.InterestDtos.Sum(
                    x =>
                    {
                        var foundInterest = pipelineResult.ScoredInterests.FirstOrDefault(y => y.Id == x.InterestId);
                        return foundInterest != null ? foundInterest.Score : 0f;
                    });
            }
        }
    }
}