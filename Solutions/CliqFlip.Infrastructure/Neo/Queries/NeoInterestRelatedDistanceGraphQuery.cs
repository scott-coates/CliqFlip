using System.Collections.Generic;
using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Queries
{
	public class NeoInterestRelatedDistanceGraphQuery
	{
        public string Slug { get; set; }
        public int Hops { get; set; }
		public List<float> Weight { get; set; } //list appears to be the only generic collection supported in neo4jclient
	}
}