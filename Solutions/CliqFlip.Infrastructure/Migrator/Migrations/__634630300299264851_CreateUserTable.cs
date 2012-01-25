using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634630300299264851)]
    public class __634630300299264851_CreateUserTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Users", new Column[] { 
                new Column("Id", System.Data.DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Username", System.Data.DbType.String, ColumnProperty.NotNull),
                new Column("Email", System.Data.DbType.String, ColumnProperty.Unique),
                new Column("Password", System.Data.DbType.String, ColumnProperty.NotNull),
                new Column("Salt", System.Data.DbType.String, ColumnProperty.NotNull),
                new Column("Bio", System.Data.DbType.String, ColumnProperty.Null)
            });
        }

        public override void Down()
        {
            Database.RemoveTable("Users");
        }
    }
}
