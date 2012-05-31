using CliqFlip.Domain.Entities.Graph;
using Neo4jClient;

namespace CliqFlip.Infrastructure.NeoRelaionships
{
	public class InterestBelongsTo : Relationship,
									 IRelationshipAllowingSourceNode<GraphInterest>,
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