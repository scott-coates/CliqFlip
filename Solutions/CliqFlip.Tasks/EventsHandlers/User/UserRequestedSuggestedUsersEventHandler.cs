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
        private readonly IPusherProvider _pusherProvider;

        public UserRequestedSuggestedUsersEventHandler(IUserSearchPipeline userSearchPipeline, IUserTasks userTasks, IPusherProvider pusherProvider)
        {
            _userSearchPipeline = userSearchPipeline;
            _userTasks = userTasks;
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
                
                _userTasks.SaveSuggestedUsers(user, pipelineResult.Users);

                _pusherProvider.Trigger(new SimplePusherRequest("suggested-user-queue-" + message.Username, "update", string.Format("{{\"usersCount\": \"{0}\"}}", pipelineResult.Users.Count)));

                tx.Commit();
            }
        }
    }
}