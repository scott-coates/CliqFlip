using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.User;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class TransformUserToScoredUserFilter : ITransformUserToScoredUserFilter
    {
        public void Filter(UserSearchPipelineResult userSearchPipelineResult, UserSearchPipelineRequest request)
        {
            if (userSearchPipelineResult == null) throw new ArgumentNullException("userSearchPipelineResult");

            userSearchPipelineResult.Users = userSearchPipelineResult.UserQuery.Select(x => new UserSearchResultDto(x)).ToList();
        }
    }
}