using CliqFlip.Infrastructure.NHibernateMaps;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.NHibernate;

namespace CliqFlip.Tests.NHibernate
{
	[TestFixture]
	public class DbSchemaTest
	{
		[Test]
		public void GenerateSchema()
		{
			var cfg = NHibernateSession.Init(
				new SimpleSessionStorage(),
				new[] { "CliqFlip.Infrastructure.dll" },
				new AutoPersistenceModelGenerator().Generate(),
				"Configuration\\NHibernate.config");
			
			new SchemaExport(cfg).Create(true, false);

			NHibernateSession.Current.Dispose();
		}
	}
}
