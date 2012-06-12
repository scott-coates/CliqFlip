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
        private readonly IAddInterestCommonalityScoreFilter _addInterestCommonalityScoreFilter;
        private readonly IAddLocationScoreFilter _addLocationScoreFilter;
        private readonly IFindRelatedInterestsFromKeywordSearchFilter _findRelatedInterestsFromKeywordSearchFilter;
        private readonly IFindTargetUsersRelatedInterestsFilter _findTargetUsersRelatedInterestsFilter;
        private readonly ILimitByInterestFilter _limitByInterestFilter;
        private readonly IScoreExplicitSearchInterestFilter _scoreExplicitSearchInterestFilter;
        private readonly IScoreRelatedInterestFilter _scoreRelatedInterestFilter;
        private readonly IUserRepository _userRepository;
        private readonly IUserToScoredUserTransformFilter _userToScoredUserTransformFilter;

        public UserSearchPipeline(IUserRepository userRepository,
                                  IFindTargetUsersRelatedInterestsFilter findTargetUsersRelatedInterestsFilter,
                                  ILimitByInterestFilter limitByInterestFilter,
                                  IScoreRelatedInterestFilter scoreRelatedInterestFilter,
                                  IUserToScoredUserTransformFilter userToScoredUserTransformFilter,
                                  IAddInterestCommonalityScoreFilter addInterestCommonalityScoreFilter,
                                  IAddLocationScoreFilter addLocationScoreFilter,
                                  IFindRelatedInterestsFromKeywordSearchFilter findRelatedInterestsFromKeywordSearchFilter,
                                  IScoreExplicitSearchInterestFilter scoreExplicitSearchInterestFilter)
        {
            _userRepository = userRepository;
            _findTargetUsersRelatedInterestsFilter = findTargetUsersRelatedInterestsFilter;
            _limitByInterestFilter = limitByInterestFilter;
            _scoreRelatedInterestFilter = scoreRelatedInterestFilter;
            _userToScoredUserTransformFilter = userToScoredUserTransformFilter;
            _addInterestCommonalityScoreFilter = addInterestCommonalityScoreFilter;
            _addLocationScoreFilter = addLocationScoreFilter;
            _findRelatedInterestsFromKeywordSearchFilter = findRelatedInterestsFromKeywordSearchFilter;
            _scoreExplicitSearchInterestFilter = scoreExplicitSearchInterestFilter;
        }

        #region IUserSearchPipeline Members

        public UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null, LocationData data = null)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll(), LocationData = data };

            /************ Constraints ******************/

            //run filter to query potential interests based on the user's list of interests 
            _findTargetUsersRelatedInterestsFilter.Filter(retVal, user);

            //run filter to query potential interests based on what was explicitly searched for
            _findRelatedInterestsFromKeywordSearchFilter.Filter(retVal, additionalSearch);

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal);

            /************ Transform ******************/

            //transform users to found dtos
            _userToScoredUserTransformFilter.Filter(retVal);

            /************ Sort ******************/

            //score interests based on user
            _scoreRelatedInterestFilter.Filter(retVal);

            //score interests based on explicit search
            _scoreExplicitSearchInterestFilter.Filter(retVal);

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

        #endregion
    }
}