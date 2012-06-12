﻿using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IFindRelatedInterestsFromKeywordSearchFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult, IList<string> slugs );
    }
}