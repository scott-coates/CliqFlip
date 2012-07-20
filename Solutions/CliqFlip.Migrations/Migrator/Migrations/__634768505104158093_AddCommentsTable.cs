#region

using System;
using System.Data;
using Migrator.Framework;

#endregion

namespace CliqFlip.Migrations.Migrator.Migrations
{
    [Migration(634768505104158093)]
    public class __634768505104158093_AddCommentsTable : Migration
    {
        private const String FK_Comments_Posts = "FK_Comments_Posts";
        private const String FK_Comments_Users = "FK_Comments_Users";

        public override void Up()
        {
            //create posts table
            Database.AddTable(
                "Comments", new[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("PostId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("UserId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("PostCommentOrder", DbType.Int32, ColumnProperty.NotNull),
                    new Column("CommentText", DbType.String, MigrationConstants.NVarCharMax, ColumnProperty.NotNull),
                    new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull)
                });

            Database.AddForeignKey(FK_Comments_Posts, "Comments", "PostId", "Posts", "Id");
            Database.AddForeignKey(FK_Comments_Users, "Comments", "UserId", "Users", "Id");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Comments", FK_Comments_Posts);
            Database.RemoveForeignKey("Comments", FK_Comments_Users);

            Database.RemoveTable("Comments");
        }
    }
}