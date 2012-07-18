using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.NHibernate.Maps;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.NHibernate;

namespace CliqFlip.Tests.NHibernate
{
	[TestFixture]
	[Ignore]
	public class MappingTest
	{
		#region Setup/Teardown

		[SetUp]
		public void GenerateSchema()
		{
			//just remember to turn hbm2ddl.auto off
			Configuration cfg = NHibernateSession.Init(
				new SimpleSessionStorage(),
				new[] { "CliqFlip.Infrastructure.dll" },
				new AutoPersistenceModelGenerator().Generate(),
				"Configuration\\NHibernate.config");

			new SchemaExport(cfg).Execute(true, true, false);
		}

		[TearDown]
		public void TearDown()
		{
			NHibernateSession.Current.Flush();
			NHibernateSession.Current.Dispose();
		}

		#endregion
	}
}