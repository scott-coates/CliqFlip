using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public class UserSearchPipelineResult
    {
        public IQueryable<User> UserQuery { get; set; }
        public IList<UserSearchResultDto> Users { get; set; }
        public IList<ScoredRelatedInterestDto> ScoredInterests { get; set; }
        public IList<WeightedRelatedInterestDto> RelatedInterests { get; set; }

        public UserSearchPipelineResult()
        {
            RelatedInterests = new List<WeightedRelatedInterestDto>();
        }
    }
}