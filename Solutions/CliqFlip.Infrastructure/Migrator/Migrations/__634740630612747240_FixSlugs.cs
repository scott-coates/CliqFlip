using System;
using System.Data;
using System.Globalization;
using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634740630612747240)]
	public class __634740630612747240_FixSlugs : Migration
	{
		public override void Up()
		{
			//some of the very first interests were not slugged
			Database.ExecuteNonQuery("UPDATE Interests SET Slug = REPLACE(LOWER(Slug), ' ', '-') WHERE ID <= 10");
		}

		public override void Down()
		{
		}
	}
}