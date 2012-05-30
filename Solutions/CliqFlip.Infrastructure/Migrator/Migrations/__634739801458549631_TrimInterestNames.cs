using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634739801458549631)]
	public class __634739801458549631_TrimInterestNames : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("UPDATE Interests SET Name = LTRIM(RTRIM(Name))");
		}

		public override void Down()
		{
		}
	}
}