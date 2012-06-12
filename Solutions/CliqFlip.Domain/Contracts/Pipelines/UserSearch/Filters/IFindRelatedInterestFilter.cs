using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IFindRelatedInterestFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult, User user = null, IList<string> additionalSearch = null);
    }
}