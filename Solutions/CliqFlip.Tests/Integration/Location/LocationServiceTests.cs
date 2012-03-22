using Castle.Windsor;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Email;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Location.Interfaces;
using CliqFlip.Infrastructure.NHibernate.Maps;
using CliqFlip.Web.Mvc.CastleWindsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace CliqFlip.Tests.Integration.Location
{
	[TestFixture]
	public class LocationServiceTests
	{
		private ILocationService _locationService;
		private IRepository<MajorLocation> _majorLocationRepo;

		[SetUp]
		public void Setup()
		{
			_majorLocationRepo = new LinqRepository<MajorLocation>();
			_locationService = new YahooGeoLocationService(_majorLocationRepo);

			IWindsorContainer container = new WindsorContainer();

			ComponentRegistrar.AddComponentsTo(container);

			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

			NHibernateSession.Init(
				new SimpleSessionStorage(),
				new[] { "CliqFlip.Infrastructure.dll" },
				new AutoPersistenceModelGenerator().Generate(),
				"Configuration\\NHibernate.config");
			NHibernateSession.Current.BeginTransaction();
		}

		[TearDown]
		public void TearDown()
		{
			NHibernateSession.Current.Transaction.Commit();
			NHibernateSession.Current.Flush();
			NHibernateSession.Current.Dispose();
		}

		#region Major Location tests

		[TestCase(33.627533F, -117.8734F, "orange-county")]
		[Ignore]
		public void CanFindNearestMajorCity(float lat, float lng, string locationName)
		{
			var majorCity = _locationService.GetNearestMajorCity(lat, lng);
		}

		#endregion
	}
}
