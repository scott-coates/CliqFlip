#region

using System;
using System.Data;
using System.Globalization;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634653694060482576)]
    public class __634653694060482576_AddActivity : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("CreateDate", DbType.DateTime, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("LastActivity", DbType.DateTime, ColumnProperty.Null));
            Database.Update("Users", new[] { "CreateDate", "LastActivity" }, new[] { DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) });
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "CreateDate");
            Database.RemoveColumn("Users", "LastActivity");
        }
    }
}