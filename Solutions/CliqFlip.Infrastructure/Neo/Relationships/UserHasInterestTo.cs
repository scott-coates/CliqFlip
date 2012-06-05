using CliqFlip.Infrastructure.Migrator.Migrations;
using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Relationships
{
	public class UserHasInterestTo : Relationship<UserHasInterestTo.Payload>,
										  IRelationshipAllowingSourceNode<NeoUser>,
										  IRelationshipAllowingTargetNode<NeoInterest>
	{
		public const string TypeKey = "USER_HAS_INTEREST";

		public override string RelationshipTypeKey
		{
			get { return TypeKey; }
		}

		public UserHasInterestTo(NodeReference targetNode, Payload hasInterestPayload)
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
}