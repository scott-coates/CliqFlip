using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634653560469431604)]
	public class __634653560469431604_AddBlog : Migration
    {
        public override void Up()
        {
			Database.AddColumn("Users", new Column("SiteUrl", System.Data.DbType.String, 1024, ColumnProperty.Null));
			Database.AddColumn("Users", new Column("FeedUrl", System.Data.DbType.String, 1024, ColumnProperty.Null));
        }

        public override void Down()
        {
			Database.RemoveColumn("Users", "SiteUrl");
			Database.RemoveColumn("Users", "FeedUrl");
        }
    }
}
