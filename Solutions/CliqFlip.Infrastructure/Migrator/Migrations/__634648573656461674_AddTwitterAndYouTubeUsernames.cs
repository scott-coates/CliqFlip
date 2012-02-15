using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634648573656461674)]
    public class __634648573656461674_AddTwitterAndYouTubeUsernames : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("TwitterUsername", System.Data.DbType.String, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("YouTubeUsername", System.Data.DbType.String, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "TwitterUsername");
            Database.RemoveColumn("Users", "YouTubeUsername");
        }
    }
}
