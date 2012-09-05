using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Messaging.Commands.User;
using CliqFlip.Messaging.Events.User;
using Facebook;
using MassTransit;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.EventsHandlers.User
{
    public class UserCreatedEventHandler : Consumes<UserCreatedEvent>.All
    {
        private readonly IUserTasks _userTasks;

        public UserCreatedEventHandler(IUserTasks userTasks)
        {
            _userTasks = userTasks;
        }

        public void Consume(UserCreatedEvent message)
        {
            using (var tx = NHibernateSession.Current.Transaction)
            {
                tx.Begin();

                _userTasks.Create(message.Username,message.Location,message.Interests);

                tx.Commit();
            }
        }
    }
}