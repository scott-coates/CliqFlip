using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634740026479549542)]
	public class __634740026479549542_MoveCurrentInterestsToNeo : Migration
	{
		public override void Up()
		{
			var db = (CliqFlipTransformationProvider)Database;
			int total = int.Parse(db.ExecuteScalar("SELECT COUNT(*) FROM INTERESTS").ToString());
			int counter = 1;

			Console.WriteLine("---- Total: {0} ----", total);
			using (IDataReader reader = db.ExecuteQuery("SELECT * FROM INTERESTS"))
			{
				while (reader.Read())
				{
					var graphInterest = new GraphInterest
					{
						Name = reader["Name"].ToString(),
						SqlId = int.Parse(reader["Id"].ToString()),
						Slug = reader["Slug"].ToString(),
						Description = reader["Description"].ToString(),
						IsMainCategory = reader["IsMainCategory"].ToString() == "1"
					};

					var ixEntry = new IndexEntry
					{
						Name = "Interests",
						KeyValues = new[]
						{
							new KeyValuePair<string, object>("Name", graphInterest.Name),
							new KeyValuePair<string, object>("Slug", graphInterest.Slug),
							new KeyValuePair<string, object>("SqlId", graphInterest.SqlId)
						}
					};

					db.GraphClient.Create(graphInterest, new[] { new InterestBelongsTo(db.GraphClient.RootNode) }, new[] { ixEntry });
					Console.WriteLine("---- Created number: {0} of {1} ----", counter++, total);
				}
			}
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider)Database;
			List<Node<GraphInterest>> graphInterests = db.GraphClient
				.RootNode
				.StartCypher("n")
				.Match("n <-[:INTEREST_BELONGS_TO]-(x)")
				.Return<Node<GraphInterest>>("x")
				.ResultSet
				.ToList();

			int total = graphInterests.Count();
			int counter = 1;

			Console.WriteLine("---- Total: {0} ----", total);

			foreach (var graphInterest in graphInterests)
			{
				db.GraphClient.Delete(graphInterest.Reference, DeleteMode.NodeAndRelationships);
				Console.WriteLine("---- Deleted number: {0} of {1} ----", counter++, total);
			}
		}

		#region Nested type: GraphInterest

		public class GraphInterest //don't use the real graphInterest as it may change in the future 
		{
			public string Name { get; set; }
			public int SqlId { get; set; }
			public string Slug { get; set; }
			public string Description { get; set; }
			public bool IsMainCategory { get; set; }
		}

		#endregion

		#region Nested type: InterestBelongsTo

		public class InterestBelongsTo : Relationship, IRelationshipAllowingSourceNode<GraphInterest>, IRelationshipAllowingTargetNode<RootNode>
		{
			public const string TypeKey = "INTEREST_BELONGS_TO";

			public override string RelationshipTypeKey
			{
				get { return TypeKey; }
			}

			public InterestBelongsTo(NodeReference targetNode)
				: base(targetNode)
			{
			}
		}

		#endregion
	}
}