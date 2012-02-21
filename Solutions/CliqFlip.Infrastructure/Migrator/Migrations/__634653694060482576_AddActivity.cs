using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634653694060482576)]
	public class __634653694060482576_AddActivity : Migration
	{
		public override void Up()
		{
			Database.AddColumn("Users", new Column("CreateDate", System.Data.DbType.DateTime, ColumnProperty.Null));
			Database.AddColumn("Users", new Column("LastActivity", System.Data.DbType.DateTime, ColumnProperty.Null));
			Database.Update("Users", new[] { "CreateDate", "LastActivity" }, new[] { DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) });
		}

		public override void Down()
		{
			Database.RemoveColumn("Users", "CreateDate");
			Database.RemoveColumn("Users", "LastActivity");
		}
	}
}
