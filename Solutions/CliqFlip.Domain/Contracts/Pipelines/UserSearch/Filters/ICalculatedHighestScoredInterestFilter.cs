using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface ICalculatedHighestScoredInterestFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult);
    }
}