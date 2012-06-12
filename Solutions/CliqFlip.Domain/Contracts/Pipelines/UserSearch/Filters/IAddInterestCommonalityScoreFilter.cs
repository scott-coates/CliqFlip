﻿using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface IAddInterestCommonalityScoreFilter
    {
        void Filter(UserSearchPipelineResult pipelineResult);
    }
}