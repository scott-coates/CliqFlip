#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634630300299264851)]
    public class __634630300299264851_CreateUserTable : Migration
    {
        public override void Up()
        {
            Database.AddTable(
                "Users", new Column[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("Username", DbType.String, ColumnProperty.NotNull),
                    new Column("Email", DbType.String, ColumnProperty.Unique),
                    new Column("Password", DbType.String, ColumnProperty.NotNull),
                    new Column("Salt", DbType.String, ColumnProperty.NotNull),
                    new Column("Bio", DbType.String, MigrationConstants.NVarCharMax, ColumnProperty.Null),
                    new Column("Headline", DbType.String, ColumnProperty.Null)
                });
        }

        public override void Down()
        {
            Database.RemoveTable("Users");
        }
    }
}