using System;
using System.Threading;
using Castle.Windsor;
using CliqFlip.Infrastructure.CastleWindsor;
using MassTransit;
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
                new GenericRepositoriesInstaller(),
                new CustomRepositoriesInstaller(),
                new MessagingSubscriberInstaller(),
                new TasksInstaller(),
                new CommandsInstaller()
                );

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