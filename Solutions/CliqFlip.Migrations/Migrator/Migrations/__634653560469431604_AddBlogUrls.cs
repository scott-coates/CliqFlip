#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634653560469431604)]
    public class __634653560469431604_AddBlogUrls : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("SiteUrl", DbType.String, 1024, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("FeedUrl", DbType.String, 1024, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "SiteUrl");
            Database.RemoveColumn("Users", "FeedUrl");
        }
    }
}