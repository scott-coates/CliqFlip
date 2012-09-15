using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using Utilities.DataTypes.ExtensionMethods;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class FindTargetUsersRelatedInterestsFilter : IFindTargetUsersRelatedInterestsFilter
    {
        private readonly IInterestRepository _interestRepository;

        public FindTargetUsersRelatedInterestsFilter(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, UserSearchPipelineRequest request)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (request == null) throw new ArgumentNullException("request");
            if (request.User == null) throw new ArgumentNullException("request","User required");

            var relatedInterestDtos = _interestRepository.GetRelatedInterests(request.User.Interests.Select(x => x.Interest.Slug).ToList()).ToList();

            pipelineResult.RelatedInterests.AddRange(relatedInterestDtos);
        }
    }
}