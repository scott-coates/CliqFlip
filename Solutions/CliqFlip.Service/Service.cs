using System;
using System.Threading;
using Castle.Windsor;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Infrastructure.CastleWindsor;
using CliqFlip.Infrastructure.NHibernate.Maps;
using CliqFlip.Tasks.CommandHandlers.User;
using CommonServiceLocator.WindsorAdapter;
using MassTransit;
using Microsoft.Practices.ServiceLocation;
using SharpArch.NHibernate;
using Topshelf;
using Topshelf.Logging;

namespace CliqFlip.Service
{
    internal class Service : ServiceControl
    {
        private static readonly LogWriter _log = HostLogger.Get<Service>();
        private readonly IWindsorContainer _container = new WindsorContainer();
        private IServiceBus _bus;

        public bool Start(HostControl hostControl)
        {
            _log.Info("Service Starting...");

            _container.Install(
                new FacilityInstaller(),
                new EventStoreInstaller(),
                new GenericRepositoriesInstaller(),
                new CustomRepositoriesInstaller(),
                new MessagingSubscriberInstaller(),
                new TasksInstaller(),
                new CommandsInstaller()
                );

            NHibernateSession.Init(
               new SimpleSessionStorage(),
                new[] { "CliqFlip.Infrastructure.dll" },
                new AutoPersistenceModelGenerator().Generate(),
                "Configuration/NHibernate.config");

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(_container));
            _container.Resolve<IServiceBus>();

            _bus = _container.Resolve<IServiceBus>();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Service Stopped");

            _container.Release(_bus);
            _container.Dispose();

            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            _log.Info("Service Paused");

            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            _log.Info("Service Continued");

            return true;
        }
    }
}