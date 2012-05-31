using CliqFlip.Infrastructure.Neo.NodeTypes;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Relationships
{
	public class InterestBelongsTo : Relationship,
									 IRelationshipAllowingSourceNode<NeoInterest>,
									 IRelationshipAllowingTargetNode<RootNode>
	{
		public const string TypeKey = "INTEREST_BELONGS_TO";

		public override string RelationshipTypeKey
		{
			get { return TypeKey; }
		}

		public InterestBelongsTo(NodeReference targetNode): base(targetNode)
		{
		}
	}
}