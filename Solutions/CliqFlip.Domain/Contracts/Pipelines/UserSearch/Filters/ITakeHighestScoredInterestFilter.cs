using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface ITakeHighestScoredInterestFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult);
    }
}