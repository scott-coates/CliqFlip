using System.Collections.Generic;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IUserSearchPipeLineFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request);
    }
}