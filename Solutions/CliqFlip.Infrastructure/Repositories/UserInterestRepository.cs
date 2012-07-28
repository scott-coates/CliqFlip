using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Neo.Entities;
using CliqFlip.Infrastructure.Neo.Queries;
using CliqFlip.Infrastructure.Neo.Relationships;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4jClient.Gremlin;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class UserInterestRepository : LinqRepository<UserInterest>, IUserInterestRepository
	{
		private readonly IGraphClient _graphClient;

		public UserInterestRepository(IGraphClient graphClient)
		{
			_graphClient = graphClient;
		}

		#region IUserInterestRepository Members

        public IQueryable<PopularInterestDto> GetMostPopularInterests()
		{
			var popularInterests =
				FindAll().ToList()
					.GroupBy(x => x.Interest)
					.Select(x => new { x.Key, Count = x.Count() })
					.OrderByDescending(x => x.Count)
					.Take(10).ToList();

			return popularInterests.Select(x => new PopularInterestDto(x.Key.Id, x.Key.Name, x.Key.Slug, x.Count)).AsQueryable();
		}

        public IQueryable<WeightedRelatedInterestDto> GetInterestsInCommon(User viewingUser, User user)
	    {
            const string queryText = @"
                START n = node:users({p0})
                MATCH n-[:USER_HAS_INTEREST]->(i)-[r:INTEREST_RELATES_TO*0.." + Constants.INTEREST_MAX_HOPS + @"]-(x)<-[:USER_HAS_INTEREST]-(u)
                WHERE u.SqlId = {p1}
                RETURN DISTINCT
                    x.SqlId AS SqlId,
                    x.Slug AS Slug,
                    x.IsMainCategory AS IsMainCategory,
                    extract(p in r : p.Weight) AS Weight
                ORDER BY x.Slug";

            var query = new CypherQuery(
                queryText,
                new Dictionary<string, object>
                {
                    {"p0", string.Format("sqlid:({0})", viewingUser.Id)},
                    {"p1", user.Id}
                },
                CypherResultMode.Projection);

            var relatedInterests = _graphClient.ExecuteGetCypherResults<NeoInterestRelatedDistanceGraphQuery>(query);

            var retVal = relatedInterests
                .Select(x => new WeightedRelatedInterestDto(x.SqlId, x.Weight, x.Slug, x.IsMainCategory))
                .AsQueryable();

            return retVal;
	    }

	    public override UserInterest SaveOrUpdate(UserInterest entity)
		{
			bool @new = entity.Id == 0;

			UserInterest retVal = base.SaveOrUpdate(entity);

			if (@new)
			{
				Node<NeoUser> userNode = GetUserNode(retVal);
				Node<NeoInterest> interestNode = _graphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("sqlid:{0}", retVal.Interest.Id)).First();

				_graphClient.CreateRelationship(userNode.Reference,
												new UserHasInterestTo(interestNode.Reference,
																	  new UserHasInterestTo.Payload
																	  {
																		  Passion = retVal.Options.Passion,
																		  SqlId = retVal.Id
																	  }));
			}
			else
			{
				//TODO look into relationship indexes - currently QueryIndex() only returns Nodes not relationships
				var userInterestRelationship = GetUserInterestRelationship(retVal);

				_graphClient.Update(userInterestRelationship.Reference, x =>
				{
					x.SqlId = retVal.Id;
					x.Passion = retVal.Options.Passion;
				});
			}

			return retVal;
		}

		private RelationshipInstance<UserHasInterestTo.Payload> GetUserInterestRelationship(UserInterest retVal)
		{
			Node<NeoUser> userNode = GetUserNode(retVal);
			IGremlinRelationshipQuery<UserHasInterestTo.Payload> userInterestQuery = userNode
				.OutE<UserHasInterestTo.Payload>(UserHasInterestTo.TypeKey, x => x.SqlId == retVal.Id);

			RelationshipInstance<UserHasInterestTo.Payload> userInterestRelationship = _graphClient
				.ExecuteGetAllRelationshipsGremlin<UserHasInterestTo.Payload>(userInterestQuery.QueryText, userInterestQuery.QueryParameters)
				.First();
			return userInterestRelationship;
		}

		public override void Delete(UserInterest target)
		{
			base.Delete(target);
			var userInterestRelationship = GetUserInterestRelationship(target);
			_graphClient.DeleteRelationship(userInterestRelationship.Reference);
		}

		#endregion

		private Node<NeoUser> GetUserNode(UserInterest retVal)
		{
			return _graphClient.QueryIndex<NeoUser>("users", IndexFor.Node, string.Format("sqlid:{0}", retVal.User.Id)).First();
		}
	}
}