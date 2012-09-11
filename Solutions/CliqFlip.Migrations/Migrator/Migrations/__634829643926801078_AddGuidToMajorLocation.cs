#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634829643926801078)]
    public class __634829643926801078_AddNameUser : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", "FirstName", DbType.String, ColumnProperty.Null);
            Database.AddColumn("Users", "LastName", DbType.String, ColumnProperty.Null);
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "FirstName");
            Database.RemoveColumn("Users", "LastName");
        }
    }
}