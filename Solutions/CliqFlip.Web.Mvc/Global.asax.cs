using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Amazon.S3;
using Amazon.SimpleEmail;
using Castle.Windsor;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.NHibernate.Maps;
using CliqFlip.Web.Mvc.CastleWindsor;
using CliqFlip.Web.Mvc.Controllers;
using CliqFlip.Web.Mvc.Security.Attributes;
using CommonServiceLocator.WindsorAdapter;
using Elmah;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Cfg;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.Castle;
using SharpArch.Web.Mvc.ModelBinder;
using log4net.Config;

namespace CliqFlip.Web.Mvc
{
	// CliqFlip.Web.Mvc.CastleWindsor


	/// <summary>
	/// Represents the MVC Application
	/// </summary>
	/// <remarks>
	/// For instructions on enabling IIS6 or IIS7 classic mode, 
	/// visit http://go.microsoft.com/?LinkId=9394801
	/// </remarks>
	public class MvcApplication : HttpApplication
	{
		private WebSessionStorage webSessionStorage;

		/// <summary>
		/// Due to issues on IIS7, the NHibernate initialization must occur in Init().
		/// But Init() may be invoked more than once; accordingly, we introduce a thread-safe
		/// mechanism to ensure it's only initialized once.
		/// See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
		/// </summary>
		public override void Init()
		{
			base.Init();
			webSessionStorage = new WebSessionStorage(this);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			NHibernateInitializer.Instance().InitializeNHibernateOnce(InitialiseNHibernateSessions);
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			// Useful for debugging
			//TODO: put some UID here so we can track this with what conversation's generate as bugs from our error page
			Exception ex = Server.GetLastError();
			var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
		}

		// ReSharper disable InconsistentNaming
		protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs args)
			// ReSharper restore InconsistentNaming
		{
			//TODO: Consider using filter configs
			Exception exception = args.Exception;
			bool include = (
			               	exception is CriticalException ||
			               	exception is AmazonS3Exception ||
			               	exception is AmazonSimpleEmailServiceException
			               	||
			               	(
			               		exception is AggregateException &&
			               		exception.ToString().Contains("Amazon")
			               	)
			               );

			if (!include)
			{
				args.Dismiss();
			}
		}

		protected void Application_Start()
		{
			XmlConfigurator.Configure();

			ViewEngines.Engines.Clear();

			ViewEngines.Engines.Add(new RazorViewEngine());

			ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

			ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());

			InitializeServiceLocator();

			AreaRegistration.RegisterAllAreas();
			RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);

			RegisterGlobalFilters(GlobalFilters.Filters);
		}

		private static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new RequireAuthenticationAttribute());
		}

		/// <summary>
		/// Instantiate the container and add all Controllers that derive from
		/// WindsorController to the container.  Also associate the Controller
		/// with the WindsorContainer ControllerFactory.
		/// </summary>
		protected virtual void InitializeServiceLocator()
		{
			IWindsorContainer container = new WindsorContainer();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

			container.RegisterControllers(typeof (HomeController).Assembly);
			ComponentRegistrar.AddComponentsTo(container);

			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
		}

		private void InitialiseNHibernateSessions()
		{
			NHibernateSession.ConfigurationCache =
				new NHibernateConfigurationFileCache(new[] {typeof (User).Assembly.GetName().Name});

			Configuration cfg = NHibernateSession.Init(
				webSessionStorage,
				new[] {Server.MapPath("~/bin/CliqFlip.Infrastructure.dll")},
				new AutoPersistenceModelGenerator().Generate(),
				Server.MapPath("~/Configuration/NHibernate.config"));
		}
	}
}