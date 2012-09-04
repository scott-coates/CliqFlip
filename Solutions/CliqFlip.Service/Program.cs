using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace CliqFlip.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Service>();
                x.RunAsLocalSystem();
                x.SetServiceName("CliqFlip.Service");
            });
        }
    }
}
