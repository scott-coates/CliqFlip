using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IFindRelatedInterestFilter
    {
        UserSearchPipelineResult Filter(UserSearchPipelineResult pipelineResult, User user = null, IList<string> additionalSearch = null);
    }
}