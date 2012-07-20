using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Relationships
{
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
}