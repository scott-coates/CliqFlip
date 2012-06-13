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

			Console.WriteLine("---- Total Nodes: {0} ----", total);
			using (IDataReader reader = db.ExecuteQuery("SELECT * FROM INTERESTS"))
			{
				while (reader.Read())
				{
					var graphInterest = new NeoInterest
					{
						Name = reader["Name"].ToString(),
						SqlId = int.Parse(reader["Id"].ToString()),
						Slug = reader["Slug"].ToString(),
						Description = reader["Description"].ToString(),
                        IsMainCategory = (bool)reader["IsMainCategory"]
					};

					var ixEntry = new IndexEntry
					{
						Name = "interests",
						KeyValues = new[]
						{
							new KeyValuePair<string, object>("name", graphInterest.Name),
							new KeyValuePair<string, object>("slug", graphInterest.Slug),
							new KeyValuePair<string, object>("sqlid", graphInterest.SqlId)
						}
					};

					db.GraphClient.Create(graphInterest, new[] { new InterestBelongsTo(db.GraphClient.RootNode) }, new[] { ixEntry });
					Console.WriteLine("---- Created number: {0} of {1} Nodes ----", counter++, total);
				}
			}

			//Add existing parent/child relationships
			total = int.Parse(db.ExecuteScalar("SELECT COUNT(*) FROM INTERESTS WHERE ParentInterestId IS NOT NULL").ToString());
			counter = 1;

			Console.WriteLine("---- Total Relationships: {0} ----", total);
			using (IDataReader reader = db.ExecuteQuery("SELECT * FROM INTERESTS WHERE ParentInterestId IS NOT NULL"))
			{
				while (reader.Read())
				{
					int sqlId = int.Parse(reader["Id"].ToString());
					int parentId = int.Parse(reader["ParentInterestId"].ToString());

					var childNod = db.GraphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("sqlid:{0}", sqlId)).First();
					var parentNod = db.GraphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("sqlid:{0}", parentId)).First();

					db.GraphClient.CreateRelationship(childNod.Reference, new InterestRelatesTo(parentNod.Reference, new InterestRelatesTo.Payload { Weight = .25f }));

					Console.WriteLine("---- Created number: {0} of {1} Relationships ----", counter++, total);
				}
			}
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider)Database;
			List<Node<NeoInterest>> graphInterests = db.GraphClient
				.RootNode
				.StartCypher("n")
				.Match("n <-[:INTEREST_BELONGS_TO]-(x)")
				.Return<Node<NeoInterest>>("x")
				.Results
				.ToList();

			int total = graphInterests.Count();
			int counter = 1;

			Console.WriteLine("---- Total Interests: {0} ----", total);

			foreach (var graphInterest in graphInterests)
			{
				db.GraphClient.Delete(graphInterest.Reference, DeleteMode.NodeAndRelationships);
				Console.WriteLine("---- Deleted number: {0} of {1} Interests----", counter++, total);
			}
		}

		#region Nested type: GraphInterest

		public class NeoInterest //don't use the real NeoInterest as it may change in the future 
		{
			public string Name { get; set; }
			public int SqlId { get; set; }
			public string Slug { get; set; }
			public string Description { get; set; }
			public bool IsMainCategory { get; set; }
		}

		#endregion

		#region Nested type: InterestBelongsTo

		public class InterestBelongsTo : Relationship, IRelationshipAllowingSourceNode<NeoInterest>, IRelationshipAllowingTargetNode<RootNode>
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

		public class InterestRelatesTo : Relationship<InterestRelatesTo.Payload>,
									 IRelationshipAllowingSourceNode<NeoInterest>,
									 IRelationshipAllowingTargetNode<NeoInterest>
		{
			public class Payload
			{
				public float Weight { get; set; }
			}

			public const string TypeKey = "INTEREST_RELATES_TO";

			public override string RelationshipTypeKey
			{
				get { return TypeKey; }
			}

			public InterestRelatesTo(NodeReference targetNode, Payload relatesToPayload)
				: base(targetNode, relatesToPayload)
			{
			}
		}

		#endregion
	}
}