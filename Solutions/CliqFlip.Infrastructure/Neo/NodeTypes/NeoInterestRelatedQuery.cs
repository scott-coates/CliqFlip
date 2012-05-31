using System.Collections.Generic;
using CliqFlip.Infrastructure.Migrator.Migrations;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.NodeTypes
{
	public class NeoInterestRelatedQuery
	{
		public Node<NeoInterest> SearchedInterest { get; set; }
		public Node<NeoInterest> FoundInterest { get; set; }
		public float Weight { get; set; }
	}
}