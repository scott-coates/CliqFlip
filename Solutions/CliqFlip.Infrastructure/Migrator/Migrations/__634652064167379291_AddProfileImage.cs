using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634652064167379291)]
    public class __634652064167379291_AddProfileImage : Migration
    {
        public override void Up()
        {
			Database.AddColumn("Users", new Column("OriginalFileName", System.Data.DbType.String, 1024, ColumnProperty.Null));
			Database.AddColumn("Users", new Column("ThumbFileName", System.Data.DbType.String, 1024, ColumnProperty.Null));
			Database.AddColumn("Users", new Column("MediumFimcleName", System.Data.DbType.String, 1024, ColumnProperty.Null));
			Database.AddColumn("Users", new Column("FullFileName", System.Data.DbType.String, 1024, ColumnProperty.Null));
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
