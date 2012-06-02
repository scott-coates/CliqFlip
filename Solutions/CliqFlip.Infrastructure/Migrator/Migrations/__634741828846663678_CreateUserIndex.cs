using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634741828846663678)]
	public class __634741828846663678_CreateUserIndex : Migration
	{
		public override void Up()
		{
			var db = (CliqFlipTransformationProvider)Database;
			db.GraphClient.CreateIndex("users", new IndexConfiguration { Provider = IndexProvider.lucene, Type = IndexType.fulltext }, IndexFor.Node);
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider)Database;
			db.GraphClient.DeleteIndex("users", IndexFor.Node);
		}
	}
}