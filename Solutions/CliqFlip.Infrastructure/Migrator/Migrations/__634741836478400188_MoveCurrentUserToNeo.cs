using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634741836478400188)]
	public class __634741836478400188_MoveCurrentUserToNeo : Migration
	{
		public override void Up()
		{
			var db = (CliqFlipTransformationProvider)Database;
			int total = int.Parse(db.ExecuteScalar("SELECT COUNT(*) FROM Users").ToString());
			int counter = 1;

			Console.WriteLine("---- Total Users: {0} ----", total);
			using (IDataReader reader = db.ExecuteQuery("SELECT * FROM Users"))
			{
				while (reader.Read())
				{
					var neoUser = new NeoUser
					{
						SqlId = int.Parse(reader["Id"].ToString())
					};

					var ixEntry = new IndexEntry
					{
						Name = "users",
						KeyValues = new[]
						{
							new KeyValuePair<string, object>("sqlid", neoUser.SqlId)
						}
					};

					db.GraphClient.Create(neoUser, new[] { new UserBelongsTo(db.GraphClient.RootNode), }, new[] { ixEntry });
					Console.WriteLine("---- Created number: {0} of {1} Users ----", counter++, total);
				}
			}
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider)Database;
			List<Node<NeoUser>> graphInterests = db.GraphClient
				.RootNode
				.StartCypher("n")
				.Match("n <-[:USER_BELONGS_TO]-(x)")
				.Return<Node<NeoUser>>("x")
				.ResultSet
				.ToList();

			int total = graphInterests.Count();
			int counter = 1;

			Console.WriteLine("---- Total Users: {0} ----", total);

			foreach (var graphInterest in graphInterests)
			{
				db.GraphClient.Delete(graphInterest.Reference, DeleteMode.NodeAndRelationships);
				Console.WriteLine("---- Deleted number: {0} of {1} Users----", counter++, total);
			}
		}

		#region Nested type: NeoUser

		public class NeoUser //don't use the real NeoInterest as it may change in the future 
		{
			public int SqlId { get; set; }
		}

		#endregion

		#region Nested type: UserBelongsTo

		public class UserBelongsTo : Relationship, IRelationshipAllowingSourceNode<NeoUser>, IRelationshipAllowingTargetNode<RootNode>
		{
			public const string TypeKey = "USER_BELONGS_TO";

			public override string RelationshipTypeKey
			{
				get { return TypeKey; }
			}

			public UserBelongsTo(NodeReference targetNode)
				: base(targetNode)
			{
			}
		}

		#endregion
	}
}