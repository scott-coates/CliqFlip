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
        private readonly ICalculateInterestCommonalityScoreFilter _calculateInterestCommonalityScoreFilter;
        private readonly ICalculateLocationScoreFilter _calculateLocationScoreFilter;
        private readonly IFindRelatedInterestsFromKeywordSearchFilter _findRelatedInterestsFromKeywordSearchFilter;
        private readonly IFindTargetUsersRelatedInterestsFilter _findTargetUsersRelatedInterestsFilter;
        private readonly ILimitByInterestFilter _limitByInterestFilter;
        private readonly ICalculateExplicitSearchInterestScoreFilter _calculateExplicitSearchInterestScoreFilter;
        private readonly ICalculateRelatedInterestScoreFilter _calculateRelatedInterestScoreFilter;
        private readonly ICalculatedHighestScoredInterestFilter _calculatedHighestScoredInterestFilter;
        private readonly ITransformUserToScoredUserFilter _transformUserToScoredUserFilter;
        private readonly IUserRepository _userRepository;
        private readonly ISortUserScoreFilter _sortUserScoreFilter;

        public UserSearchPipeline(IUserRepository userRepository,
                                  IFindTargetUsersRelatedInterestsFilter findTargetUsersRelatedInterestsFilter,
                                  ILimitByInterestFilter limitByInterestFilter,
                                  ICalculateRelatedInterestScoreFilter calculateRelatedInterestScoreFilter,
                                  ITransformUserToScoredUserFilter transformUserToScoredUserFilter,
                                  ICalculateInterestCommonalityScoreFilter calculateInterestCommonalityScoreFilter,
                                  ICalculateLocationScoreFilter calculateLocationScoreFilter,
                                  IFindRelatedInterestsFromKeywordSearchFilter findRelatedInterestsFromKeywordSearchFilter,
                                  ICalculateExplicitSearchInterestScoreFilter calculateExplicitSearchInterestScoreFilter,
                                  ICalculatedHighestScoredInterestFilter highestScoredInterestFilter,
                                  ICalculatedHighestScoredInterestFilter calculatedHighestScoredInterestFilter,
                                  ISortUserScoreFilter sortUserScoreFilter)
        {
            _userRepository = userRepository;
            _findTargetUsersRelatedInterestsFilter = findTargetUsersRelatedInterestsFilter;
            _limitByInterestFilter = limitByInterestFilter;
            _calculateRelatedInterestScoreFilter = calculateRelatedInterestScoreFilter;
            _transformUserToScoredUserFilter = transformUserToScoredUserFilter;
            _calculateInterestCommonalityScoreFilter = calculateInterestCommonalityScoreFilter;
            _calculateLocationScoreFilter = calculateLocationScoreFilter;
            _findRelatedInterestsFromKeywordSearchFilter = findRelatedInterestsFromKeywordSearchFilter;
            _calculateExplicitSearchInterestScoreFilter = calculateExplicitSearchInterestScoreFilter;
            _calculatedHighestScoredInterestFilter = calculatedHighestScoredInterestFilter;
            _sortUserScoreFilter = sortUserScoreFilter;
        }

        #region IUserSearchPipeline Members

        public UserSearchPipelineResult Execute(User user = null, IList<string> additionalSearch = null, LocationData data = null)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll(), LocationData = data };

            /************ Find Dependent Data  ******************/

            //run filter to query potential interests based on the user's list of interests 
            _findTargetUsersRelatedInterestsFilter.Filter(retVal, user);

            //run filter to query potential interests based on what was explicitly searched for
            _findRelatedInterestsFromKeywordSearchFilter.Filter(retVal, additionalSearch);

            /************ Limits/Constraints ******************/

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal);

            /************ Transform ******************/

            //transform users to found dtos
            _transformUserToScoredUserFilter.Filter(retVal);

            /************ Calculate Signal ******************/

            //score interests based on user
            _calculateRelatedInterestScoreFilter.Filter(retVal);

            //score interests based on explicit search
            _calculateExplicitSearchInterestScoreFilter.Filter(retVal);

            //keep only the highest scored interests
            _calculatedHighestScoredInterestFilter.Filter(retVal);

            //sort users by interests points
            _calculateInterestCommonalityScoreFilter.Filter(retVal);

            _calculateLocationScoreFilter.Filter(retVal);

            /************ Sort ******************/

            //order by scores
            _sortUserScoreFilter.Filter(retVal);

            return retVal;
        }

        #endregion
    }
}