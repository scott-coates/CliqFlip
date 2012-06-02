using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Migrator.Framework;
using MigratorNeo4j.CliqFlip;
using Neo4jClient;
using Neo4jClient.Gremlin;

namespace CliqFlip.Infrastructure.Migrator.Migrations
{
	[Migration(634741839617969761)]
	public class __634741839617969761_MoveCurrentUserInterestsToNeo : Migration
	{
		public override void Up()
		{
			var db = (CliqFlipTransformationProvider) Database;
			int total = int.Parse(db.ExecuteScalar("SELECT COUNT(*) FROM UserInterests").ToString());
			int counter = 1;

			Console.WriteLine("---- Total UserInterests: {0} ----", total);
			using (IDataReader reader = db.ExecuteQuery("SELECT UserId, InterestId, Id, Passion FROM UserInterests"))
			{
				while (reader.Read())
				{
					int userId = reader.GetInt32(0);
					int interestId = reader.GetInt32(1);
					int userInterestId = reader.GetInt32(2);
					float? userInterestPassion = null;
					if(!reader.IsDBNull(3))
					{
						userInterestPassion = reader.GetFloat(3);
					}

					Node<NeoUser> userNode = db.GraphClient.QueryIndex<NeoUser>("users", IndexFor.Node, string.Format("sqlid:{0}", userId)).First();
					Node<NeoInterest> interestNode = db.GraphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("sqlid:{0}", interestId)).First();


					db.GraphClient.CreateRelationship(userNode.Reference,
					                                  new UserHastInterestTo(interestNode.Reference,
					                                                         new UserHastInterestTo.Payload
					                                                         {
					                                                         	Passion = userInterestPassion,
					                                                         	SqlId = userInterestId
					                                                         }));
					Console.WriteLine("---- Created number: {0} of {1} UserInterests ----", counter++, total);
				}
			}
		}

		public override void Down()
		{
			var db = (CliqFlipTransformationProvider) Database;
			var relationshipQuery = db
				.GraphClient
				.RootNode
				.InE("USER_BELONGS_TO")
				.OutV<NeoUser>()
				.OutE("USER_HAS_INTEREST");

			var relationShips = db.GraphClient
				.ExecuteGetAllRelationshipsGremlin(relationshipQuery.QueryText, relationshipQuery.QueryParameters)
				.ToList();


			int total = relationShips.Count;
			int counter = 1;

			Console.WriteLine("---- Total UserInterests: {0} ----", total);

			foreach (var relationShip in relationShips)
			{
				db.GraphClient.DeleteRelationship(relationShip.Reference);
				Console.WriteLine("---- Deleted number: {0} of {1} UserInterests----", counter++, total);
			}
		}

		#region Nested type: NeoInterest

		public class NeoInterest //don't use the real NeoInterest as it may change in the future 
		{
			public int SqlId { get; set; }
		}

		#endregion

		#region Nested type: NeoUser

		public class NeoUser //don't use the real NeoInterest as it may change in the future 
		{
			public int SqlId { get; set; }
		}

		#endregion

		#region Nested type: UserHastInterestTo

		public class UserHastInterestTo : Relationship<UserHastInterestTo.Payload>,
		                                  IRelationshipAllowingSourceNode<NeoUser>,
		                                  IRelationshipAllowingTargetNode<NeoInterest>
		{
			public const string TypeKey = "INTEREST_RELATES_TO";

			public override string RelationshipTypeKey
			{
				get { return TypeKey; }
			}

			public UserHastInterestTo(NodeReference targetNode, Payload hasInterestPayload)
				: base(targetNode, hasInterestPayload)
			{
			}

			#region Nested type: Payload

			public class Payload
			{
				public int SqlId { get; set; }
				public float? Passion { get; set; }
			}

			#endregion
		}

		#endregion
	}
}