using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634630305581406972)]
    public class __634630305581406972_CreateUserInterestTable : Migration
    {
        private const String FK_UserInterest_Users = "FK_UserInterests_Users";
        private const String FK_UserInterest_Interests = "FK_UserInterests_Interests";
        public override void Up()
        {
            Database.AddTable("UserInterests", new Column[] { 
                new Column("Id", System.Data.DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("InterestId", System.Data.DbType.Int32, ColumnProperty.ForeignKey),
                new Column("UserId", System.Data.DbType.Int32, ColumnProperty.ForeignKey),
                new Column("SocialityPoints", System.Data.DbType.Int32, ColumnProperty.Null),
                new Column("Passion", System.Data.DbType.Int32, ColumnProperty.Null) //TODO: Check with team
            });

            //add the foreign key constraints

            Database.AddForeignKey(FK_UserInterest_Users, "UserInterests", "UserId", "Users", "Id", ForeignKeyConstraint.Cascade);
            Database.AddForeignKey(FK_UserInterest_Interests, "UserInterests", "InterestId", "Interests", "Id", ForeignKeyConstraint.Cascade);

        }

        public override void Down()
        {
            Database.RemoveForeignKey("UserInterests", FK_UserInterest_Users);
            Database.RemoveForeignKey("UserInterests", FK_UserInterest_Interests);
            Database.RemoveTable("UserInterests");
        }
    }
}
