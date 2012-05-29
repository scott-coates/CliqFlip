using System;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CliqFlip.Domain.Common;
using Neo4jClient;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.Web.Mvc.Castle;
using CliqFlip.Infrastructure.Authentication;

namespace CliqFlip.Web.Mvc.CastleWindsor
{
	public class ComponentRegistrar
	{
		public static void AddComponentsTo(IWindsorContainer container)
		{
			//TODO:use xml config for environment-specific
			AddFacilitiesTo(container);
			AddGenericRepositoriesTo(container);
			AddCustomRepositoriesTo(container);
			AddQueryObjectsTo(container);
			AddTasksTo(container);
			AddCommandsTo(container);
			AddUserTo(container);
		}

		private static void AddFacilitiesTo(IWindsorContainer container)
		{
			container.AddFacility<FactorySupportFacility>();
		}

		private static void AddUserTo(IWindsorContainer container)
		{
			//http://stackoverflow.com/a/1081803/173957
			container.Register(Component.For<IPrincipal>()
			                   	.LifeStyle.PerWebRequest
			                   	.UsingFactoryMethod(() => HttpContext.Current.User));
		}

		private static void AddTasksTo(IWindsorContainer container)
		{
			container.Register(
				AllTypes
					.FromAssemblyNamed("CliqFlip.Tasks")
					.Pick()
					.WithService.FirstNonGenericCoreInterface("CliqFlip.Domain"));
		}

		private static void AddCustomRepositoriesTo(IWindsorContainer container)
		{
			container.Register(
				AllTypes
					.FromAssemblyNamed("CliqFlip.Infrastructure")
					.Pick()
					.WithService.FirstNonGenericCoreInterface("CliqFlip.Domain")
					.WithService.FirstNonGenericCoreInterface("CliqFlip.Infrastructure"));

			container
				.Register(Component
							.For<IGraphClient>()
							.LifeStyle.Singleton.UsingFactoryMethod(() =>
							{
								var graphClient =
									new GraphClient(new
														Uri(WebConfigurationManager
														.ConnectionStrings[Constants.GRAPH_URL].ConnectionString));
								graphClient.Connect();
								return graphClient;
							}));
		}

		private static void AddGenericRepositoriesTo(IWindsorContainer container)
		{
			container.Register(
				Component.For(typeof (IQuery<>))
					.ImplementedBy(typeof (NHibernateQuery<>))
					.Named("NHibernateQuery"));

			container.Register(
				Component.For(typeof (IEntityDuplicateChecker))
					.ImplementedBy(typeof (EntityDuplicateChecker))
					.Named("entityDuplicateChecker"));

			container.Register(
				Component.For(typeof (INHibernateRepository<>))
					.ImplementedBy(typeof (NHibernateRepository<>))
					.Named("nhibernateRepositoryType")
					.Forward(typeof (IRepository<>)));

			container.Register(
				Component.For(typeof (INHibernateRepositoryWithTypedId<,>))
					.ImplementedBy(typeof (NHibernateRepositoryWithTypedId<,>))
					.Named("nhibernateRepositoryWithTypedId")
					.Forward(typeof (IRepositoryWithTypedId<,>)));

			container.Register(
				Component.For(typeof (ILinqRepository<>))
					.ImplementedBy(typeof (LinqRepository<>))
					.Named("linqRepositoryType"));

			container.Register(
				Component.For(typeof (ISessionFactoryKeyProvider))
					.ImplementedBy(typeof (DefaultSessionFactoryKeyProvider))
					.Named("sessionFactoryKeyProvider"));

			container.Register(
				Component.For(typeof (ICommandProcessor))
					.ImplementedBy(typeof (CommandProcessor))
					.Named("commandProcessor"));
		}

		private static void AddQueryObjectsTo(IWindsorContainer container)
		{
			container.Register(
				AllTypes.FromAssemblyNamed("CliqFlip.Web.Mvc")
					.Pick()
					.WithService.FirstInterface());
		}

		private static void AddCommandsTo(IWindsorContainer container)
		{
			container.Register(
				AllTypes.FromAssemblyNamed("CliqFlip.Tasks")
					.Pick()
					.WithService.FirstInterface());
		}
	}
}