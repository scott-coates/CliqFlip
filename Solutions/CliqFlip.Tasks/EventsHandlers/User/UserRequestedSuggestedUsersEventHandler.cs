using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Messaging.Commands.User;
using CliqFlip.Messaging.Events.User;
using Facebook;
using MassTransit;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.EventsHandlers.User
{
    public class UserRequestedSuggestedUsersEventHandler : Consumes<UserRequestedSuggestedUsersEvent>.All
    {
        private readonly IUserSearchPipeline _userSearchPipeline;
        private readonly IUserTasks _userTasks;
        private readonly IUserInterestTasks _userInterestTasks;

        public UserRequestedSuggestedUsersEventHandler(IUserSearchPipeline userSearchPipeline, IUserTasks userTasks, IUserInterestTasks userInterestTasks)
        {
            _userSearchPipeline = userSearchPipeline;
            _userTasks = userTasks;
            _userInterestTasks = userInterestTasks;
        }

        public void Consume(UserRequestedSuggestedUsersEvent message)
        {
            var user = _userTasks.GetUser(message.Username);
            var pipelineRequest = new UserSearchPipelineRequest { User = user, LocationData = user.Location.Data };
            var pipelineResult = _userSearchPipeline.Execute(pipelineRequest);
            foreach (var userSearchResult in pipelineResult.Users)
            {
                var commonInterest = _userInterestTasks.GetInterestsInCommon(user, _userTasks.GetUser(userSearchResult.Username));
                userSearchResult.CommonInterestCount = commonInterest.Count(x => x.IsExactMatch);
                userSearchResult.RelatedInterestCount = commonInterest.Count(x => !x.IsExactMatch);
                userSearchResult.InterestsInCommon = commonInterest
                    .OrderByDescending(x => x.Score)
                    .Select(
                        x => new UserSearchResultDto.InterestInCommonDto
                        {
                            Name = x.Name,
                            IsExactMatch = x.IsExactMatch
                        })
                    .ToList();
            }
            _userTasks.SaveSuggestedUsers(user, pipelineResult.Users);
        }
    }
}