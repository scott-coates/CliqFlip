using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
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
        private readonly IAddInterestCommonalityScoreFilter _addInterestCommonalityScoreFilter;
        private readonly IAddLocationScoreFilter _addLocationScoreFilter;

        public UserSearchPipeline(IUserRepository userRepository, IFindRelatedInterestFilter findRelatedInterestFilter, ILimitByInterestFilter limitByInterestFilter, IScoreRelatedInterestFilter scoreRelatedInterestFilter, IUserToScoredUserTransformFilter userToScoredUserTransformFilter, IAddInterestCommonalityScoreFilter addInterestCommonalityScoreFilter, IAddLocationScoreFilter addLocationScoreFilter)
        {
            _userRepository = userRepository;
            _findRelatedInterestFilter = findRelatedInterestFilter;
            _limitByInterestFilter = limitByInterestFilter;
            _scoreRelatedInterestFilter = scoreRelatedInterestFilter;
            _userToScoredUserTransformFilter = userToScoredUserTransformFilter;
            _addInterestCommonalityScoreFilter = addInterestCommonalityScoreFilter;
            _addLocationScoreFilter = addLocationScoreFilter;
        }

        public UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null, LocationData data = null)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll(), LocationData = data };

            //run filter to query potential interests 
            _findRelatedInterestFilter.Filter(retVal, user, additionalSearch);

            //score interests and related
            _scoreRelatedInterestFilter.Filter(retVal);

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal);

            //transform users to found dtos
            _userToScoredUserTransformFilter.Filter(retVal);

            //sort users by interests points
            _addInterestCommonalityScoreFilter.Filter(retVal);

            _addLocationScoreFilter.Filter(retVal);

            //order by scores
            retVal.Users = retVal
                .Users
                .OrderByDescending(x => x.Score)
                .ToList();

            return retVal;
        }
    }
}