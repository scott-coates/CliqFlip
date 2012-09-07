using System.Collections.Generic;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.ReadModels;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using ServiceStack.Redis;

namespace CliqFlip.Infrastructure.Repositories
{
    public class SuggestedUserRepository : ISuggestedUserRepository
    {
        private readonly IRedisClient _redisClient;

        public SuggestedUserRepository(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public void Save(User user, IList<UserSearchResultDto> users)
        {
            using (var redisUsers = _redisClient.GetTypedClient<UserSearchResultDto>())
            {
                var set = redisUsers.SortedSets["suggested-user:" + user.Id];
                foreach (var userSearchResultDto in users)
                {
                    redisUsers.AddItemToSortedSet(set, userSearchResultDto, userSearchResultDto.Score);
                }
            }
        }
    }
}