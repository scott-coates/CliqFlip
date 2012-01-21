using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.NHibernateMaps;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CliqFlip.Tests.Generation.Schema
{
	[TestFixture]
	public class DbSchemaTest
	{
		[Test]
		public void GenerateSchema()
		{
			var cfg = new Configuration();
			cfg.AddAssembly(typeof (User).Assembly);
			cfg.addcon
			cfg.Configure();

			new SchemaExport(cfg).Execute(true,true,false);
		}
	}
}
