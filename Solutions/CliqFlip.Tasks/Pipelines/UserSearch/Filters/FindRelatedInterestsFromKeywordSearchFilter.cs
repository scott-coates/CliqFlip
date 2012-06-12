using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using Utilities.DataTypes.ExtensionMethods;

namespace CliqFlip.Tasks.Pipelines.UserSearch.Filters
{
    public class FindRelatedInterestsFromKeywordSearchFilter : IFindRelatedInterestsFromKeywordSearchFilter
    {
        private readonly IInterestRepository _interestRepository;

        public FindRelatedInterestsFromKeywordSearchFilter(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public void Filter(UserSearchPipelineResult pipelineResult, IList<string> slugs)
        {
            if (pipelineResult == null) throw new ArgumentNullException("pipelineResult");
            if (slugs == null) throw new ArgumentNullException("slugs");

            var relatedInterestDtos = _interestRepository.GetRelatedInterests(slugs).ToList();
            relatedInterestDtos.ForEach(x => x.ExplicitSearch = true);
            pipelineResult.RelatedInterests.AddRange(relatedInterestDtos);
        }
    }
}