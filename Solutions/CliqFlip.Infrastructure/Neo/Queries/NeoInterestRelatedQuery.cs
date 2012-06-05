using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Queries
{
	public class NeoInterestRelatedQuery
	{
		public Node<NeoInterest> FoundInterest { get; set; }
		public float Weight { get; set; }
	}
}