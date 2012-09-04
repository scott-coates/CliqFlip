using System;
using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using MassTransit;
using MassTransit.Transports;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class MessagingPublisherInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEndpointCache>()
                    .UsingFactoryMethod(
                        () => EndpointCacheFactory.New(
                            x =>
                            {
                                x.SetCreateMissingQueues(true);
                                x.UseRabbitMq();
                            })).LifeStyle.Singleton,
                Component.For<IEndpoint>().UsingFactoryMethod(
                    (k, c) =>
                    k.Resolve<IEndpointCache>()
                        .GetEndpoint(new Uri(ConfigurationManager.ConnectionStrings[Constants.RABBIT_MQ_URI].ConnectionString)))
                    .LifeStyle.PerWebRequest);
        }
    }
}