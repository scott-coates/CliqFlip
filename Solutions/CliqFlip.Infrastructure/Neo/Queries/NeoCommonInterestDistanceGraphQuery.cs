using System.Collections.Generic;
using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Queries
{
	public class NeoCommonInterestDistanceGraphQuery
	{
        public string Name { get; set; }
        public int SqlId { get; set; }
        public List<float> Weight { get; set; }
        public bool IsMainCategory { get; set; }
	}
}