using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class FindRelatedInterestFilter : IFindRelatedInterestFilter
    {
        private readonly IInterestRepository _interestRepository;

        public FindRelatedInterestFilter(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, User user = null, IList<string> additionalSearch = null)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");

            var slugList = new List<string>();

            if (user != null && user.Interests != null)
            {
                slugList.AddRange(user.Interests.Select(x => x.Interest.Slug));
            }

            if (additionalSearch != null)
            {
                slugList.AddRange(additionalSearch);
            }

            var relatedInterestDtos = _interestRepository.GetRelatedInterests(slugList).ToList();

            pipelineResult.RelatedInterests = relatedInterestDtos;
        }
    }
}