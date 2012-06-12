using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public interface IUserSearchPipeline
    {
        UserSearchPipelineResult Execute(UserSearchPipelineRequest request);
    }
}