#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634648573656461674)]
    public class __634648573656461674_AddTwitterAndYouTubeUsernames : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("TwitterUsername", DbType.String, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("YouTubeUsername", DbType.String, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "TwitterUsername");
            Database.RemoveColumn("Users", "YouTubeUsername");
        }
    }
}