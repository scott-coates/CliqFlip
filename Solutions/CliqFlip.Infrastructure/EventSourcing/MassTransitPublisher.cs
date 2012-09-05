using System;
using EventStore;
using EventStore.Dispatcher;
using Magnum.Reflection;
using MassTransit;

namespace CliqFlip.Infrastructure.EventSourcing
{
    public class MassTransitPublisher : IDispatchCommits
    {
        private readonly IServiceBus _bus;
        private const string _publishName = "PublishEvent";

        public MassTransitPublisher(IServiceBus bus)
        {
            _bus = bus;
        }

        void IDispatchCommits.Dispatch(Commit commit)
        {
            commit.Events.ForEach(@event => this.FastInvoke(_publishName, @event.Body));
        }

        private void PublishEvent<T>(T message) where T : class
        {
            _bus.Publish(message);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}