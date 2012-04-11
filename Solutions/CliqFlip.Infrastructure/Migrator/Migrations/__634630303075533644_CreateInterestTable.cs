using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634630303075533644)]
    public class __634630303075533644_CreateInterestTable : Migration
    {
        private const String FK_Interests_Interests = "FK_Interests_Interests";
        public override void Up()
        {
            Database.AddTable("Interests", new Column[] {
                new Column("Id", System.Data.DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Name", System.Data.DbType.String, ColumnProperty.NotNull),
                new Column("Description", System.Data.DbType.String, ColumnProperty.Null),
                new Column("Slug", System.Data.DbType.String, ColumnProperty.Unique),
                new Column("ParentInterestId", System.Data.DbType.Int32, ColumnProperty.Null)
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
