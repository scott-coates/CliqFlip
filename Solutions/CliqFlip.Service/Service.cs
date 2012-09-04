using System;
using System.Threading;
using Castle.Windsor;
using CliqFlip.Infrastructure.CastleWindsor;
using Topshelf;
using Topshelf.Logging;

namespace CliqFlip.Service
{
    internal class Service : ServiceControl
    {
        private static readonly LogWriter _log = HostLogger.Get<Service>();
        private readonly IWindsorContainer _container = new WindsorContainer();

        public bool Start(HostControl hostControl)
        {
            _log.Info("Service Starting...");

            ComponentRegistrar.AddComponentsTo(_container);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Service Stopped");

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