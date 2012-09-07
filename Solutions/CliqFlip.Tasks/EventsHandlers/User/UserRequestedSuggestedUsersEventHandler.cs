using System.Linq;
using CliqFlip.Domain.Contracts.Pipelines.UserSearch;
using CliqFlip.Domain.Contracts.Tasks.Entities;
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

        public UserRequestedSuggestedUsersEventHandler(IUserSearchPipeline userSearchPipeline, IUserTasks userTasks)
        {
            _userSearchPipeline = userSearchPipeline;
            _userTasks = userTasks;
        }

        public void Consume(UserRequestedSuggestedUsersEvent message)
        {
            var user = _userTasks.GetUser(message.Username);
            var pipelineRequest = new UserSearchPipelineRequest { User = user, LocationData = user.Location.Data };
            var pipelineResult = _userSearchPipeline.Execute(pipelineRequest);
            _userTasks.SaveSuggestedUsers(user, pipelineResult.Users);
        }
    }
}