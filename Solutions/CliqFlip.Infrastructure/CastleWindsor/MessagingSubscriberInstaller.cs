﻿using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using CliqFlip.Infrastructure.EventSourcing;
using EventStore.Dispatcher;
using MassTransit;
using MassTransit.Log4NetIntegration;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class MessagingSubscriberInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IServiceBus>()
                    .UsingFactoryMethod(
                        () => ServiceBusFactory.New(
                            sbc =>
                            {
                                sbc.ReceiveFrom(ConfigurationManager.ConnectionStrings[Constants.RABBIT_MQ_URI].ConnectionString);
                                sbc.UseRabbitMqRouting();
                                sbc.UseLog4Net("log4net.xml");
                                sbc.Subscribe(c => c.LoadFrom(container));
                            })).LifeStyle.Singleton);

            container.Register(
                Component.For<IDispatchCommits>()
                    .UsingFactoryMethod((k, c) => new MassTransitPublisher(k.Resolve<IServiceBus>()))
                    .LifeStyle.Singleton);
        }
    }
}