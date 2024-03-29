using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch
{
    public class UserSearchPipeline : IUserSearchPipeline
    {
        private readonly IAssignInterestCommonalityScoreFilter _assignInterestCommonalityScoreFilter;
        private readonly IAssignLocationScoreFilter _assignLocationScoreFilter;
        private readonly ICalculateExplicitSearchInterestScoreFilter _calculateExplicitSearchInterestScoreFilter;
        private readonly ICalculateRelatedInterestScoreFilter _calculateRelatedInterestScoreFilter;
        private readonly ICalculateHighestScoredInterestFilter _calculateHighestScoredInterestFilter;
        private readonly IFindRelatedInterestsFromKeywordSearchFilter _findRelatedInterestsFromKeywordSearchFilter;
        private readonly IFindTargetUsersRelatedInterestsFilter _findTargetUsersRelatedInterestsFilter;
        private readonly ILimitByInterestFilter _limitByInterestFilter;
        private readonly ILimitByTargetUserFilter _limitByTargetUserFilter;
        private readonly ISortUserScoreFilter _sortUserScoreFilter;
        private readonly ITransformUserToScoredUserFilter _transformUserToScoredUserFilter;
        private readonly IUserRepository _userRepository;
        private readonly ICalculateMainCategoryInterestScoreFilter _calculateMainCategoryInterestScoreFilter;
        private readonly ILimitByExplicitSearchScoreFilter _limitByExplicitSearchScoreFilter;
        private readonly ILimitByCloseInterestFilter _limitByCloseInterestFilter;

        public UserSearchPipeline(IUserRepository userRepository,
                                  IFindTargetUsersRelatedInterestsFilter findTargetUsersRelatedInterestsFilter,
                                  ILimitByInterestFilter limitByInterestFilter,
                                  ICalculateRelatedInterestScoreFilter calculateRelatedInterestScoreFilter,
                                  ITransformUserToScoredUserFilter transformUserToScoredUserFilter,
                                  IAssignInterestCommonalityScoreFilter assignInterestCommonalityScoreFilter,
                                  IAssignLocationScoreFilter assignLocationScoreFilter,
                                  IFindRelatedInterestsFromKeywordSearchFilter findRelatedInterestsFromKeywordSearchFilter,
                                  ICalculateExplicitSearchInterestScoreFilter calculateExplicitSearchInterestScoreFilter,
                                  ICalculateHighestScoredInterestFilter calculateHighestScoredInterestFilter,
                                  ISortUserScoreFilter sortUserScoreFilter,
                                  ILimitByTargetUserFilter limitByTargetUserFilter,
                                  ILimitByExplicitSearchScoreFilter limitByExplicitSearchScoreFilter,
                                  ICalculateMainCategoryInterestScoreFilter calculateMainCategoryInterestScoreFilter,
                                  ILimitByCloseInterestFilter limitByCloseInterestFilter)
        {
            _userRepository = userRepository;
            _findTargetUsersRelatedInterestsFilter = findTargetUsersRelatedInterestsFilter;
            _limitByInterestFilter = limitByInterestFilter;
            _calculateRelatedInterestScoreFilter = calculateRelatedInterestScoreFilter;
            _transformUserToScoredUserFilter = transformUserToScoredUserFilter;
            _assignInterestCommonalityScoreFilter = assignInterestCommonalityScoreFilter;
            _assignLocationScoreFilter = assignLocationScoreFilter;
            _findRelatedInterestsFromKeywordSearchFilter = findRelatedInterestsFromKeywordSearchFilter;
            _calculateExplicitSearchInterestScoreFilter = calculateExplicitSearchInterestScoreFilter;
            _calculateHighestScoredInterestFilter = calculateHighestScoredInterestFilter;
            _sortUserScoreFilter = sortUserScoreFilter;
            _limitByTargetUserFilter = limitByTargetUserFilter;
            _limitByExplicitSearchScoreFilter = limitByExplicitSearchScoreFilter;
            _calculateMainCategoryInterestScoreFilter = calculateMainCategoryInterestScoreFilter;
            _limitByCloseInterestFilter = limitByCloseInterestFilter;
        }

        #region IUserSearchPipeline Members

        public UserSearchPipelineResult Execute(UserSearchPipelineRequest request)
        {
            //set flags
            var explicitSearch = request.InterestSearch != null;

            //start with list of users
            var retVal = new UserSearchPipelineResult { UserQuery = _userRepository.FindAll() };

            /************ Find Dependent Data  ******************/

            //run filter to query potential interests based on the user's list of interests 
            _findTargetUsersRelatedInterestsFilter.Filter(retVal, request);

            //run filter to query potential interests based on what was explicitly searched for
            if (explicitSearch)
            {
                _findRelatedInterestsFromKeywordSearchFilter.Filter(retVal, request);
            }

            /************ Calculate Signal Data ******************/

            //score interests based on user
            _calculateRelatedInterestScoreFilter.Filter(retVal, request);

            //score interests based on explicit search
            if (explicitSearch)
            {
                _calculateExplicitSearchInterestScoreFilter.Filter(retVal, request);
            }

            //main categories get less points - too vague
            _calculateMainCategoryInterestScoreFilter.Filter(retVal, request);

            //keep only the highest scored interests
            _calculateHighestScoredInterestFilter.Filter(retVal, request);

            /************ Limits/Constraints ******************/

            //if explicit search, hide less relevant interests
            if (explicitSearch)
            {
                _limitByExplicitSearchScoreFilter.Filter(retVal, request);
            }
            else
            {
                //only keep 'close' interests
                _limitByCloseInterestFilter.Filter(retVal, request);
            }

            //filter users by interests and related
            _limitByInterestFilter.Filter(retVal, request);

            //dont show the target user in results
            _limitByTargetUserFilter.Filter(retVal, request);

            /************ Transform ******************/

            //transform users to found dtos
            _transformUserToScoredUserFilter.Filter(retVal, request);

            /************ Assign User Score ******************/

            //sort users by interests points
            _assignInterestCommonalityScoreFilter.Filter(retVal, request);

            _assignLocationScoreFilter.Filter(retVal, request);

            /************ Sort ******************/

            //order by scores
            _sortUserScoreFilter.Filter(retVal, request);

            return retVal;
        }

        #endregion
    }
}