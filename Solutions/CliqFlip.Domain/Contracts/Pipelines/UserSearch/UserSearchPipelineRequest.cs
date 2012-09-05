using System.Collections.Generic;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Pipelines.UserSearch
{
    public class UserSearchPipelineRequest
    {
        public LocationData LocationData { get; set; }
        public User User { get; set; }
        public IList<string> InterestSearch { get; set; }
    }
}