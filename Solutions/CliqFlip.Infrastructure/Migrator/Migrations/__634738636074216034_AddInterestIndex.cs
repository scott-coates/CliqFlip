﻿using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634738636074216034)]
	public class __634738636074216034_AddInterestIndex : Migration
	{
		public override void Up()
		{
			var db = (CliqFlipTransformationProvider)Database;
			db.GraphClient.CreateIndex("Interests", new IndexConfiguration { Provider = IndexProvider.lucene, Type = IndexType.fulltext }, IndexFor.Node);
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider)Database;
			db.GraphClient.DeleteIndex("Interests", IndexFor.Node);
		}
	}
}