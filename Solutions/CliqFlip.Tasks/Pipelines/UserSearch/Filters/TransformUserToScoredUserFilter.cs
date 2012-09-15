using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class TransformUserToScoredUserFilter : ITransformUserToScoredUserFilter
    {
        private readonly IUserInterestTasks _userInterestTasks;

        public TransformUserToScoredUserFilter(IUserInterestTasks userInterestTasks)
        {
            _userInterestTasks = userInterestTasks;
        }

        public void Filter(UserSearchPipelineResult userSearchPipelineResult, UserSearchPipelineRequest request)
        {
            if (userSearchPipelineResult == null) throw new ArgumentNullException("userSearchPipelineResult");
            if (request == null) throw new ArgumentNullException("request");
            if (request.User == null) throw new ArgumentNullException("request.user");

            userSearchPipelineResult.Users = userSearchPipelineResult
                .UserQuery
                .ToList()
                .Select(x =>
                {
                    var retVal = new UserSearchResultDto(x);
                    var commonInterest = _userInterestTasks.GetInterestsInCommon(request.User, x);
                    retVal.DirectInterestCount = commonInterest.Count(y => y.IsExactMatch);
                    retVal.IndirectInterestCount = commonInterest.Count(y => !y.IsExactMatch);
                    retVal.CommonInterestCount = retVal.DirectInterestCount + retVal.IndirectInterestCount;
                    retVal.InterestsInCommon = commonInterest
                        .OrderByDescending(y => y.Score)
                        .Select(
                            y => new UserSearchResultDto.InterestInCommonDto
                            {
                                Name = y.Name,
                                IsExactMatch = y.IsExactMatch
                            })
                        .ToList();
                    return retVal;
                }).ToList();
        }
    }
}