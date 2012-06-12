using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch
{
    public class UserSearchPipeline : IUserSearchPipeline
    {
        private readonly IUserRepository _userRepository;

        public UserSearchPipeline(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult
            {
                UserQuery = _userRepository.FindAll()
            };

            //run filter to query potential interests 

            //get and filter by list of interests

            //either inspect the current user or a list of search terms

            //

            return retVal;
        }
    }
}