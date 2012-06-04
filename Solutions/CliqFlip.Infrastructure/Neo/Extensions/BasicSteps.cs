using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Neo4jClient.Gremlin;

namespace CliqFlip.Infrastructure.Neo.Extensions
{
	public static class BasicSteps
	{
		public static IGremlinRelationshipQuery OutE(this IGremlinQuery query, string label)
		{
			var filter = new Filter
			{
				ExpressionType = ExpressionType.Equal,
				PropertyName = "label",
				Value = label
			};

			var newQuery = query.AddFilterBlock(".outE", new[] { filter }, StringComparison.Ordinal);
		}
	}
}
