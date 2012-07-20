#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634654274650150024)]
    public class __634654274650150024_AddFacebookUsername : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("FacebookUsername", DbType.String, 1024, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "FacebookUsername");
        }
    }
}