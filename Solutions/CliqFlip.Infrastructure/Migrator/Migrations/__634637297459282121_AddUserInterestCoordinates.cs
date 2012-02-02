using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634637297459282121)]
    public class __634637297459282121_AddUserInterestCoordinates : Migration
    {
        public override void Up()
        {
            Database.AddColumn("UserInterests", "XAxis", System.Data.DbType.Double, ColumnProperty.Null);
            Database.AddColumn("UserInterests", "YAxis", System.Data.DbType.Double, ColumnProperty.Null); 
        }
        public override void Down()
        {
            Database.RemoveColumn("UserInterests", "XAxis");
            Database.RemoveColumn("UserInterests", "YAxis");
        }
    }
}
