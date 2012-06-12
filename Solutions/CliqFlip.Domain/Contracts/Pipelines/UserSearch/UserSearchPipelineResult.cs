using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public class UserSearchPipelineResult
    {
        public IQueryable<User> UserQuery { get; set; } 
        public IEnumerable<UserSearchResultDto> Users { get; set; }
        public IEnumerable<ScoredRelatedInterestDto> ScoredInterests { get; set; }
        public List<WeightedRelatedInterestDto> RelatedInterests { get; set; }
    }
}