using System.Linq;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Messaging.Commands.User;
using CliqFlip.Messaging.Events.User;
using Facebook;
using MassTransit;
using PusherRESTDotNet;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.EventsHandlers.User
{
    public class UserRegistrationEventHandler : Consumes<UserExaminedEvent>.All, Consumes<UserFoundGeneralDataEvent>.All, Consumes<UserFoundInterestDataEvent>.All
    {
        private readonly IPusherProvider _pusherProvider;

        public UserRegistrationEventHandler(IPusherProvider pusherProvider)
        {
            _pusherProvider = pusherProvider;
        }

        public void Consume(UserExaminedEvent message)
        {
            _pusherProvider.Trigger(new SimplePusherRequest("registration-" + message.Username, "reg-examine", ""));
        }

        public void Consume(UserFoundGeneralDataEvent message)
        {
            _pusherProvider.Trigger(new SimplePusherRequest("registration-" + message.Username, "reg-find-compat-people", ""));
        }

        public void Consume(UserFoundInterestDataEvent message)
        {
            _pusherProvider.Trigger(new SimplePusherRequest("registration-" + message.Username, "reg-organizing", ""));
        }
    }
}