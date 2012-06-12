﻿using System.Collections.Generic;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch.Filters
{
    public interface ILimitByInterestFilter
    {
        UserSearchPipelineResult Filter(UserSearchPipelineResult pipelineResult);
    }
}