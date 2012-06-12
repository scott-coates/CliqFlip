using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public interface IUserSearchPipeline
    {
        UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null);
    }
}