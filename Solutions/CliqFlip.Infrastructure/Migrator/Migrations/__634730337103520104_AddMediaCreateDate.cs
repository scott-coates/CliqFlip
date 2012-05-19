using System;
using System.Data;
using System.Globalization;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634730337103520104)]
	public class __634730337103520104_AddMediaCreateDate : Migration
	{
		public override void Up()
		{
			Database.AddColumn("Media", "CreateDate", DbType.DateTime, ColumnProperty.Null);

			Database.Update("Media", new[] { "CreateDate" }, new[] { DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) });

			Database.ChangeColumn("Media", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull));
		}

		public override void Down()
		{
			Database.RemoveColumn("Media", "CreateDate");
		}
	}
}