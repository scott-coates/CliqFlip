using System.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Common;
using MassTransit;
using SharpArch.Web.Mvc.Castle;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class TasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               AllTypes
                   .FromAssemblyNamed("CliqFlip.Tasks")
                   .Pick()
                   .WithService.FirstNonGenericCoreInterface("CliqFlip.Domain"));
        }
    }
}