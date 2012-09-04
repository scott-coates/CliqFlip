using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Topshelf;
using Topshelf.Logging;

namespace CliqFlip.Service
{
    internal class Service : ServiceControl
    {
        private static readonly LogWriter _log = HostLogger.Get<Service>();

        public bool Start(HostControl hostControl)
        {
            _log.Info("SampleService Starting...");

            hostControl.RequestAdditionalTime(TimeSpan.FromSeconds(10));

            Thread.Sleep(1000);

            ThreadPool.QueueUserWorkItem(
                x =>
                {
                    Thread.Sleep(3000);

                    _log.Info("Requesting stop");

                    hostControl.Stop();
                });
            _log.Info("SampleService Started");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("SampleService Stopped");

            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            _log.Info("SampleService Paused");

            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            _log.Info("SampleService Continued");

            return true;
        }
    }
}