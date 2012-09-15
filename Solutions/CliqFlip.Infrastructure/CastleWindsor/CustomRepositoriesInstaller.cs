using System;
using System.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Common;
using MassTransit;
using Neo4jClient;
using PusherRESTDotNet;
using ServiceStack.Redis;
using SharpArch.Web.Mvc.Castle;

namespace CliqFlip.Infrastructure.CastleWindsor
{
    public class CustomRepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("CliqFlip.Infrastructure")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("CliqFlip.Domain")
                    .WithService.FirstNonGenericCoreInterface("CliqFlip.Infrastructure"));

            container
                .Register(
                    Component
                        .For<IGraphClient>()
                        .LifeStyle.Singleton.UsingFactoryMethod(
                            () =>
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings[Constants.GRAPH_URL].ConnectionString;
                                var rootUri = new Uri(connectionString);
                                var graphClient = new GraphClient(rootUri) { EnableSupportForNeo4jOnHeroku = true };
                                graphClient.Connect();
                                return graphClient;
                            }));

            container
                .Register(
                    Component
                        .For<IPusherProvider>()
                        .LifeStyle.Singleton.UsingFactoryMethod(
                            () =>
                            {
                                var pusher = new PusherProvider(
                                    ConfigurationManager.AppSettings[Constants.PUSHER_APP_ID], ConfigurationManager.AppSettings[Constants.PUSHER_APP_KEY], ConfigurationManager.AppSettings[Constants.PUSHER_APP_SECRET]);
                                return pusher;
                            }));

            container
                .Register(
                    Component
                        .For<IRedisClient>()
                        .LifeStyle.Singleton.UsingFactoryMethod(
                            () =>
                            {
                                var client = new RedisClient("ubuntu", 6379);
                                client.DbSize.ToString();//will cause exception right here right now
                                return client;
                            }));

        }
    }
}