using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Pipelines;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Dtos.User;

namespace CliqFlip.Tasks.Pipelines
{
    public class UserSearchPipeline : IUserSearchPipeline
    {
        //start with list of interests

        //either inspect the current user or a list of search terms

        //
        public IEnumerable<UserSearchPipelineResult> Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}