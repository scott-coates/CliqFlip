using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public interface IUserSearchPipeline
    {
        IEnumerable<UserSearchPipelineResult> Execute();
    }
}