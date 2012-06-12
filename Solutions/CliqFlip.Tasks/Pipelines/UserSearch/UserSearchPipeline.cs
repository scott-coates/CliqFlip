using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch
{
    public class UserSearchPipeline : IUserSearchPipeline
    {
        private readonly IUserRepository _userRepository;
        private readonly IFindRelatedInterestFilter _findRelatedInterestFilter;
        private readonly ILimitByInterestFilter _limitByInterestFilter;
        private readonly IScoreRelatedInterestFilter _scoreRelatedInterestFilter;
        private readonly IUserToScoredUserTransformFilter _userToScoredUserTransformFilter;
        private readonly ISortByRelatedInterestFilter _sortByRelatedInterestFilter;

        public UserSearchPipeline(IUserRepository userRepository, IFindRelatedInterestFilter findRelatedInterestFilter, ILimitByInterestFilter limitByInterestFilter, IScoreRelatedInterestFilter scoreRelatedInterestFilter, IUserToScoredUserTransformFilter userToScoredUserTransformFilter, ISortByRelatedInterestFilter sortByRelatedInterestFilter)
        {
            _userRepository = userRepository;
            _findRelatedInterestFilter = findRelatedInterestFilter;
            _limitByInterestFilter = limitByInterestFilter;
            _scoreRelatedInterestFilter = scoreRelatedInterestFilter;
            _userToScoredUserTransformFilter = userToScoredUserTransformFilter;
            _sortByRelatedInterestFilter = sortByRelatedInterestFilter;
        }

        public UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll() };

            //run filter to query potential interests 
            _findRelatedInterestFilter.Filter(retVal, user, additionalSearch);

            //score interests and related
            _scoreRelatedInterestFilter.Filter(retVal);

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal);

            //transform users to found dtos
            _userToScoredUserTransformFilter.Filter(retVal);

            //sort users by interests points
            _sortByRelatedInterestFilter.Filter(retVal);

            return retVal;
        }
    }
}