using CliqFlip.Infrastructure.Neo.NodeTypes;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Relationships
{
	public class InterestRelatesTo : Relationship,
	                                 IRelationshipAllowingSourceNode<NeoInterest>,
	                                 IRelationshipAllowingTargetNode<NeoInterest>
	{
		public const string TypeKey = "INTEREST_RELATES_TO";

		public override string RelationshipTypeKey
		{
			get { return TypeKey; }
		}

		public InterestRelatesTo(NodeReference targetNode, float weight)
			: base(targetNode, weight)
		{
		}
	}
}