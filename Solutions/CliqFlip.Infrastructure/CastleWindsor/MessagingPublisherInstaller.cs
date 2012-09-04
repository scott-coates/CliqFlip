using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using MassTransit;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class MessagingPublisherInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IServiceBus>()
                    .UsingFactoryMethod(
                        () => ServiceBusFactory.New(
                            sbc => sbc.UseRabbitMqRouting())).LifeStyle.Singleton);
        }
    }
}