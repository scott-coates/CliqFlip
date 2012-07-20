#region

using System;
using System.Data;
using System.Globalization;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634739921452161339)]
    public class __634739921452161339_AddDateToInterest : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Interests", "CreateDate", DbType.DateTime, ColumnProperty.Null);

            Database.Update("Interests", new[] { "CreateDate" }, new[] { DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) });

            Database.ChangeColumn("Interests", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveColumn("Interests", "CreateDate");
        }
    }
}