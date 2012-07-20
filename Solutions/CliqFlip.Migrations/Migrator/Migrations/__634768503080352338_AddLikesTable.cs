#region

using System;
using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634768503080352338)]
    public class __634768503080352338_AddLikesTable : Migration
    {
        private const String FK_Likes_Posts = "FK_Likes_Posts";
        private const String FK_Likes_Users = "FK_Likes_Users";

        public override void Up()
        {
            //create posts table
            Database.AddTable(
                "Likes", new[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("UserId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("PostId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull)
                });

            Database.AddForeignKey(FK_Likes_Posts, "Likes", "PostId", "Posts", "Id");
            Database.AddForeignKey(FK_Likes_Users, "Likes", "UserId", "Users", "Id");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Likes", FK_Likes_Posts);
            Database.RemoveForeignKey("Likes", FK_Likes_Users);

            Database.RemoveTable("Likes");
        }
    }
}