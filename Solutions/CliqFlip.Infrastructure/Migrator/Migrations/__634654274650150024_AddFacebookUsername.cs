using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634654274650150024)]
    public class __634654274650150024_AddFacebookUsername : Migration
    {
        public override void Up()
        {
			Database.AddColumn("Users", new Column("FacebookUsername", System.Data.DbType.String, 1024, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "FacebookUsername");
        }
    }
}
