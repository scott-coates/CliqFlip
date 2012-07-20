#region

using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634652064167379291)]
    public class __634652064167379291_AddProfileImage : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Users", new Column("OriginalFileName", DbType.String, 1024, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("ThumbFileName", DbType.String, 1024, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("MediumFileName", DbType.String, 1024, ColumnProperty.Null));
            Database.AddColumn("Users", new Column("FullFileName", DbType.String, 1024, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn("Users", "OriginalFileName");
            Database.RemoveColumn("Users", "ThumbFileName");
            Database.RemoveColumn("Users", "MediumFileName");
            Database.RemoveColumn("Users", "FullFileName");
        }
    }
}