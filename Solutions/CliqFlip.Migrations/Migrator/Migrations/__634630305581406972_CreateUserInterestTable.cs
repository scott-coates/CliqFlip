#region

using System;
using System.Data;
using Migrator.Framework;
using ForeignKeyConstraint = Migrator.Framework.ForeignKeyConstraint;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634630305581406972)]
    public class __634630305581406972_CreateUserInterestTable : Migration
    {
        private const String FK_UserInterest_Users = "FK_UserInterests_Users";
        private const String FK_UserInterest_Interests = "FK_UserInterests_Interests";

        public override void Up()
        {
            Database.AddTable(
                "UserInterests", new Column[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("InterestId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("UserId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("SocialityPoints", DbType.Int32, ColumnProperty.Null),
                    new Column("XAxis", DbType.Single, ColumnProperty.Null),
                    new Column("YAxis", DbType.Single, ColumnProperty.Null),
                    new Column("Passion", DbType.Single, ColumnProperty.Null) //TODO: Check with team
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