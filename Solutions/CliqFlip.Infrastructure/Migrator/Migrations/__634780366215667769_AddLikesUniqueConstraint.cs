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
    [Migration(634780366215667769)]
    public class __634780366215667769_AddLikesUniqueConstraint : Migration
    {
        public override void Up()
        {
            Database.AddUniqueConstraint("IX_Unique_Like", "Likes", "PostId", "UserId");
        }

        public override void Down()
        {
            Database.RemoveConstraint("Likes", "IX_Unique_Like");
        }
    }
}