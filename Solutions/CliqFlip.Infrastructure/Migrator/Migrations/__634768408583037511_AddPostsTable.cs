using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;
using Neo4jClient.Gremlin;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
    [Migration(634768408583037511)]
    public class __634768408583037511_AddPostsTable : Migration
    {
        private const String FK_Posts_Media = "FK_Posts_Media";
        private const String FK_Posts_Users = "FK_Posts_Users";
        private const String FK_Posts_Interests = "FK_Posts_Interests";
        private const String FK_Media_UsersInterests = "FK_Media_UsersInterests";

        public override void Up()
        {
            //create posts table
            Database.AddTable(
                "Posts", new[]
                {
                    new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("UserId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("InterestId", DbType.Int32, ColumnProperty.NotNull),
                    new Column("InterestPostOrder", DbType.Int32, ColumnProperty.NotNull),
                    new Column("Description", DbType.String, 1024, ColumnProperty.Null),
                    new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull),
                    new Column("MediumId", DbType.Int32, ColumnProperty.Null)
                });

            #region AddPostsFromMedia

            const string addPostsFromMedia =
                @"
INSERT INTO [Posts]
           ([InterestPostOrder]
           ,[Description]
           ,[UserId]
           ,[InterestId]
           ,[CreateDate]
           ,[MediumId])
     SELECT m.InterestMediumOrder, m.Description, ui.UserId, ui.InterestId, m.CreateDate, m.Id
     FROM Media m
     INNER JOIN UserInterests ui on m.UserInterestId = ui.Id
     WHERE UserInterestId IS NOT NULL";

            #endregion

            Database.ExecuteNonQuery(addPostsFromMedia);

            Database.RemoveForeignKey("Media", FK_Media_UsersInterests);

            Database.RemoveColumn("Media", "Description");
            Database.RemoveColumn("Media", "InterestMediumOrder");
            Database.RemoveColumn("Media", "UserInterestId");

            Database.AddForeignKey(FK_Posts_Media, "Posts", "MediumId", "Media", "Id");
            Database.AddForeignKey(FK_Posts_Users, "Posts", "UserId", "Users", "Id");
            Database.AddForeignKey(FK_Posts_Interests, "Posts", "InterestId", "Interests", "Id");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Posts", FK_Posts_Users);
            Database.RemoveForeignKey("Posts", FK_Posts_Media);
            Database.RemoveForeignKey("Posts", FK_Posts_Media);

            Database.AddColumn("Media", "UserInterestId", DbType.Int32, ColumnProperty.Null);
            Database.AddColumn("Media", "InterestMediumOrder", DbType.Int32, ColumnProperty.Null);
            Database.AddColumn("Media", "Description", DbType.String, 1024, ColumnProperty.Null);

            #region addMediaFromPosts

            const string addMediaFromPosts =
                @"
UPDATE m
     SET m.UserInterestId = ui.Id, m.InterestMediumOrder = p.InterestPostOrder, m.Description = p.Description
     FROM Media m 
     INNER JOIN Posts p on m.Id = p.MediumId
     INNER JOIN UserInterests ui on p.UserId = ui.UserId AND p.InterestId = ui.InterestId";

            #endregion

            Database.ExecuteNonQuery(addMediaFromPosts);

            Database.AddForeignKey(FK_Media_UsersInterests, "Media", "UserInterestId", "UserInterests", "Id");

            Database.RemoveTable("Posts");
        }
    }
}