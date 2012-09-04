using System.Configuration;
using System.Security.Principal;
using System.Web;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using MassTransit;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class UserInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //http://stackoverflow.com/a/1081803/173957
            container.Register(Component.For<IPrincipal>()
                                .LifeStyle.PerWebRequest
                                .UsingFactoryMethod(() => HttpContext.Current.User));
        
        }
    }
}