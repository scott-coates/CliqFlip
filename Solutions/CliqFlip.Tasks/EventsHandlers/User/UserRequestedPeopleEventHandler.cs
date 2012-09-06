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
    public class UserRequestedPeopleEventHandler : Consumes<UserRequestedPeopleEvent>.All
    {
        private readonly IUserSearchPipeline _userSearchPipeline;
        private readonly IUserTasks _userTasks;

        public UserRequestedPeopleEventHandler(IUserSearchPipeline userSearchPipeline, IUserTasks userTasks)
        {
            _userSearchPipeline = userSearchPipeline;
            _userTasks = userTasks;
        }

        public void Consume(UserRequestedPeopleEvent message)
        {
            var user = _userTasks.GetUser(message.Username);
            var pipelineRequest = new UserSearchPipelineRequest { User = user };
            var pipelineResult = _userSearchPipeline.Execute(pipelineRequest);
        }
    }
}