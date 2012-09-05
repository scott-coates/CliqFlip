using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class CommandsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssemblyNamed("CliqFlip.Tasks")
                    .Pick()
                    .WithService.FirstInterface());
        }
    }
}