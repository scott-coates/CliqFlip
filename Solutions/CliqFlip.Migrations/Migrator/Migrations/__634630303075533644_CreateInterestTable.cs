#region

using System;
using System.Data;
using Migrator.Framework;
using ForeignKeyConstraint = Migrator.Framework.ForeignKeyConstraint;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634630303075533644)]
    public class __634630303075533644_CreateInterestTable : Migration
    {
        private const String FK_Interests_Interests = "FK_Interests_Interests";

        public override void Up()
        {
            Database.AddTable(
                "Interests", new Column[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("Name", DbType.String, ColumnProperty.NotNull),
                    new Column("Description", DbType.String, ColumnProperty.Null),
                    new Column("Slug", DbType.String, ColumnProperty.Unique),
                    new Column("ParentInterestId", DbType.Int32, ColumnProperty.Null)
                });

            Database.AddForeignKey(FK_Interests_Interests, "Interests", "ParentInterestId", "Interests", "Id", ForeignKeyConstraint.NoAction);
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Interests", FK_Interests_Interests);
            Database.RemoveTable("Interests");
        }
    }
}