using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Messaging.Commands.User;
using CliqFlip.Messaging.Events.User;
using Facebook;
using MassTransit;
using PusherRESTDotNet;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.EventsHandlers.User
{
    public class UserRequestedSuggestedUsersEventHandler : Consumes<UserRequestedSuggestedUsersEvent>.All
    {
        private readonly IUserSearchPipeline _userSearchPipeline;
        private readonly IUserTasks _userTasks;
        private readonly IUserInterestTasks _userInterestTasks;
        private readonly IPusherProvider _pusherProvider;

        public UserRequestedSuggestedUsersEventHandler(IUserSearchPipeline userSearchPipeline, IUserTasks userTasks, IUserInterestTasks userInterestTasks, IPusherProvider pusherProvider)
        {
            _userSearchPipeline = userSearchPipeline;
            _userTasks = userTasks;
            _userInterestTasks = userInterestTasks;
            _pusherProvider = pusherProvider;
        }

        public void Consume(UserRequestedSuggestedUsersEvent message)
        {
            using (var tx = NHibernateSession.Current.Transaction)
            {
                tx.Begin();
                var user = _userTasks.GetUser(message.Username);
                var pipelineRequest = new UserSearchPipelineRequest { User = user, LocationData = user.Location.Data };
                var pipelineResult = _userSearchPipeline.Execute(pipelineRequest);
                foreach (var userSearchResult in pipelineResult.Users)
                {
                    var commonInterest = _userInterestTasks.GetInterestsInCommon(user, _userTasks.GetUser(userSearchResult.Username));
                    userSearchResult.DirectInterestCount = commonInterest.Count(x => x.IsExactMatch);
                    userSearchResult.IndirectInterestCount = commonInterest.Count(x => !x.IsExactMatch);
                    userSearchResult.CommonInterestCount = userSearchResult.DirectInterestCount + userSearchResult.IndirectInterestCount;
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

                _pusherProvider.Trigger(new SimplePusherRequest("suggested-user-queue-" + message.Username, "update", ""));

                tx.Commit();
            }
        }
    }
}