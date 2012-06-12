using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch
{
    public class UserSearchPipeline : IUserSearchPipeline
    {
        private readonly ICalculateExplicitSearchInterestScoreFilter _calculateExplicitSearchInterestScoreFilter;
        private readonly ICalculateInterestCommonalityScoreFilter _calculateInterestCommonalityScoreFilter;
        private readonly ICalculateLocationScoreFilter _calculateLocationScoreFilter;
        private readonly ICalculateRelatedInterestScoreFilter _calculateRelatedInterestScoreFilter;
        private readonly ICalculatedHighestScoredInterestFilter _calculatedHighestScoredInterestFilter;
        private readonly IFindRelatedInterestsFromKeywordSearchFilter _findRelatedInterestsFromKeywordSearchFilter;
        private readonly IFindTargetUsersRelatedInterestsFilter _findTargetUsersRelatedInterestsFilter;
        private readonly ILimitByInterestFilter _limitByInterestFilter;
        private readonly ILimitByTargetUserFilter _limitByTargetUserFilter;
        private readonly ISortUserScoreFilter _sortUserScoreFilter;
        private readonly ITransformUserToScoredUserFilter _transformUserToScoredUserFilter;
        private readonly IUserRepository _userRepository;

        public UserSearchPipeline(IUserRepository userRepository,
                                  IFindTargetUsersRelatedInterestsFilter findTargetUsersRelatedInterestsFilter,
                                  ILimitByInterestFilter limitByInterestFilter,
                                  ICalculateRelatedInterestScoreFilter calculateRelatedInterestScoreFilter,
                                  ITransformUserToScoredUserFilter transformUserToScoredUserFilter,
                                  ICalculateInterestCommonalityScoreFilter calculateInterestCommonalityScoreFilter,
                                  ICalculateLocationScoreFilter calculateLocationScoreFilter,
                                  IFindRelatedInterestsFromKeywordSearchFilter findRelatedInterestsFromKeywordSearchFilter,
                                  ICalculateExplicitSearchInterestScoreFilter calculateExplicitSearchInterestScoreFilter,
                                  ICalculatedHighestScoredInterestFilter calculatedHighestScoredInterestFilter,
                                  ISortUserScoreFilter sortUserScoreFilter,
                                  ILimitByTargetUserFilter limitByTargetUserFilter)
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
            _limitByTargetUserFilter = limitByTargetUserFilter;
        }

        #region IUserSearchPipeline Members

        public UserSearchPipelineResult Execute(UserSearchPipelineRequest request)
        {
            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll() };

            /************ Find Dependent Data  ******************/

            //run filter to query potential interests based on the user's list of interests 
            _findTargetUsersRelatedInterestsFilter.Filter(retVal, request);

            //run filter to query potential interests based on what was explicitly searched for
            _findRelatedInterestsFromKeywordSearchFilter.Filter(retVal, request);

            /************ Limits/Constraints ******************/

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal, request);

            //dont show the target user in results
            _limitByTargetUserFilter.Filter(retVal,request);

            /************ Transform ******************/

            //transform users to found dtos
            _transformUserToScoredUserFilter.Filter(retVal, request);

            /************ Calculate Signal ******************/

            //score interests based on user
            _calculateRelatedInterestScoreFilter.Filter(retVal, request);

            //score interests based on explicit search
            _calculateExplicitSearchInterestScoreFilter.Filter(retVal, request);

            //keep only the highest scored interests
            _calculatedHighestScoredInterestFilter.Filter(retVal, request);

            //sort users by interests points
            _calculateInterestCommonalityScoreFilter.Filter(retVal, request);

            _calculateLocationScoreFilter.Filter(retVal, request);

            /************ Sort ******************/

            //order by scores
            _sortUserScoreFilter.Filter(retVal, request);

            return retVal;
        }

        #endregion
    }
}