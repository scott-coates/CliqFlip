﻿using System;
using CliqFlip.Infrastructure.Neo.Entities;
using Neo4jClient;

namespace CliqFlip.Infrastructure.Neo.Relationships
{
	public class InterestRelatesTo : Relationship<InterestRelatesTo.Payload>,
	                                 IRelationshipAllowingSourceNode<NeoInterest>,
	                                 IRelationshipAllowingTargetNode<NeoInterest>
	{
		public class Payload
		{
			public float Weight { get; set; }
		}

		public const string TypeKey = "INTEREST_RELATES_TO";

		public override string RelationshipTypeKey
		{
			get { return TypeKey; }
		}

		public InterestRelatesTo(NodeReference targetNode, Payload relatesToPayload)
			: base(targetNode, relatesToPayload)
		{
			Direction = RelationshipDirection.Outgoing;//point direction from source to target
            if(relatesToPayload == null || relatesToPayload.Weight <= 0)
            {
                throw new ArgumentException("A weight greater than 0 is required", "relatesToPayload");
            }
		}
	}
}