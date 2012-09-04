using System;
using System.Configuration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using MassTransit;
using Neo4jClient;
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
        }
    }
}