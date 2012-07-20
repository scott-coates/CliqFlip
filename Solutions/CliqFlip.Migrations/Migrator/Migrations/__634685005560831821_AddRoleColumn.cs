#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634685005560831821)]
    public class __634685005560831821_AddRoleColumn : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", "Role", DbType.String, 255);
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "Role");
        }
    }
}