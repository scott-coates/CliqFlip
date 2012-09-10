using System;
using System.Collections.Generic;
using System.Linq;
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
                var listId = "suggested-user:" + user.Id;
                var set = redisUsers.SortedSets[listId];
                foreach (var userSearchResultDto in users)
                {
                    redisUsers.AddItemToSortedSet(set, userSearchResultDto, userSearchResultDto.Score);
                }
                redisUsers.ExpireEntryIn(listId, TimeSpan.FromHours(1));
            }
        }

        public IQueryable<UserSearchResultDto> GetSuggestedUsers(User user)
        {
            using (var redisUsers = _redisClient.GetTypedClient<UserSearchResultDto>())
            {
                var listId = "suggested-user:" + user.Id;
                var set = redisUsers.SortedSets[listId];
                return redisUsers.GetAllItemsFromSortedSetDesc(set).AsQueryable();
            }
        }
    }
}