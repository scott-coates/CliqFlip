using System.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Common;
using MassTransit;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class WebComponentsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 AllTypes.FromAssemblyNamed("CliqFlip.Web.Mvc")
                     .Pick()
                     .WithService.FirstInterface());
        }
    }
}