using System.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Common;
using CliqFlip.Infrastructure.EventSourcing;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;
using MassTransit;
using Newtonsoft.Json;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IStoreEvents>()
                    .UsingFactoryMethod(k => GetInitializedEventStore(k.Resolve<IDispatchCommits>())),
                C<IRepository, EventStoreRepository>(),
                C<IConstructAggregates, AggregateFactory>(),
                C<IDetectConflicts, ConflictDetector>());
        }

        private static ComponentRegistration<TS> C<TS, TC>()
            where TC : TS
            where TS : class
        {
            return Component.For<TS>().ImplementedBy<TC>().LifeStyle.Transient;
        }

        private IStoreEvents GetInitializedEventStore(IDispatchCommits bus)
        {
            return Wireup.Init()
                .UsingAsynchronousDispatchScheduler(bus)
                .UsingSqlPersistence("default")
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Build();
        }
    }
}