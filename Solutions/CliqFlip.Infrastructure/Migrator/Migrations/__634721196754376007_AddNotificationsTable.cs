using System.Data;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634721196754376007)]
	public class __634721196754376007_AddNotificationsTable : Migration
	{
		public override void Up()
		{
			Database.AddTable("Notifications", new[]
			{
				new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
				new Column("Message", DbType.String, 2048, ColumnProperty.NotNull),
				new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull)
			});
		}

		public override void Down()
		{
			Database.RemoveTable("Notifications");
		}
	}
}