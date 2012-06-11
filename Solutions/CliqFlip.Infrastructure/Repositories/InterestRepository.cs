using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CliqFlip.Domain.Common;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Enums;
using CliqFlip.Domain.Extensions;
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
    public class InterestRepository : LinqRepository<Interest>, IInterestRepository
    {
        private readonly IGraphClient _graphClient;
        
        public InterestRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #region IInterestRepository Members

        public IQueryable<Interest> GetMatchingKeywords(string input)
        {
            var adHoc = new AdHoc<Interest>(s => s.Name.Contains(input) || input.Contains(s.Name));
            return FindAll(adHoc);
        }

        public IQueryable<WeightedRelatedInterestDto> GetRelatedInterests(IList<string> slugs)
        {
            const string queryText = @"
                START n = node:interests({p0})
                MATCH p = n-[r:INTEREST_RELATES_TO*0.." + Constants.INTEREST_MAX_HOPS + @"]-(x)
                RETURN x.SqlId AS SqlId, x.Slug AS Slug, extract(r in relationships(p) : r.Weight) AS Weight
                ORDER BY x.Slug";

            var query = new CypherQuery(
                queryText,
                new Dictionary<string, object>
                {
                    {"p0", string.Format("slug:({0})", string.Join(" ", slugs))}
                },
                CypherResultMode.Projection);

            var relatedInterests = _graphClient.ExecuteGetCypherResults<NeoInterestRelatedDistanceGraphQuery>(query);
            
            var retVal = relatedInterests
                .Select(x => new WeightedRelatedInterestDto(x.SqlId,x.Weight,x.Slug))
                .AsQueryable();

            return retVal;
        }

        public Interest GetByName(string name)
        {
            var withMatchingName = new AdHoc<Interest>(x => x.Name == name);
            Interest interest = FindOne(withMatchingName);

            return interest;
        }

        public IQueryable<Interest> GetMainCategoryInterests()
        {
            return FindAll(new AdHoc<Interest>(x => x.IsMainCategory)).OrderBy(x => x.Name);
        }

        public RelatedInterestListDto GetRelatedInterests(string interestSlug)
        {
            var retVal = new RelatedInterestListDto();

            Node<NeoInterest> startingRef = FindInterestNodeBySlug(interestSlug);

            Func<NeoInterest, RelatedInterestListDto.RelatedInterestDto> convert = x => new RelatedInterestListDto.RelatedInterestDto(x.SqlId, null, x.Name, x.Slug);

            retVal.OriginalInterest = convert(startingRef.Data);

            IEnumerable<NeoInterestRelatedQuery> neoRelatedInterestQuery =
                startingRef
                    .StartCypher("n")
                    .Match("n -[r:INTEREST_RELATES_TO]-(x)")
                    .Return(
                        (x, r) => new NeoInterestRelatedQuery
                        {
                            FoundInterest = x.Node<NeoInterest>(),
                            Weight = r.As<NeoInterestRelatedQuery>().Weight
                        }
                    )
                    .Results;

            if (neoRelatedInterestQuery != null)
            {
                retVal.WeightedRelatedInterestDtos = neoRelatedInterestQuery.Select(
                    x => new RelatedInterestListDto.WeightedRelatedInterestDto
                    {
                        Interest = convert(x.FoundInterest.Data),
                        Weight = x.Weight
                    }).ToList();
            }

            return retVal;
        }

        public void CreateRelationships(RelatedInterestListDto relatedInterestListDto)
        {
            Node<NeoInterest> startingRef = FindInterestNodeBySlug(relatedInterestListDto.OriginalInterest.Slug);

            IGremlinRelationshipQuery relationshipsQuery = startingRef.Reference.BothE(InterestRelatesTo.TypeKey);

            List<RelationshipInstance<InterestRelatesTo.Payload>> existingRelationships = _graphClient
                .ExecuteGetAllRelationshipsGremlin<InterestRelatesTo.Payload>(
                    relationshipsQuery.QueryText,
                    relationshipsQuery.QueryParameters)
                .ToList();

            foreach (RelatedInterestListDto.WeightedRelatedInterestDto relatedInterest in relatedInterestListDto.WeightedRelatedInterestDtos)
            {
                Node<NeoInterest> relatedNode = FindInterestNodeBySqlId(relatedInterest.Interest.Id);

                RelationshipInstance<InterestRelatesTo.Payload> existingRelationship = existingRelationships
                    .FirstOrDefault(
                        x =>
                        x.StartNodeReference == relatedNode.Reference
                        || x.EndNodeReference == relatedNode.Reference);

                if (existingRelationship != null)
                {
                    RelatedInterestListDto.WeightedRelatedInterestDto interest = relatedInterest;
                    _graphClient.Update(existingRelationship.Reference, x => x.Weight = interest.Weight);
                }
                else
                {
                    _graphClient.CreateRelationship(
                        startingRef.Reference, new InterestRelatesTo(
                                                   relatedNode.Reference, new InterestRelatesTo.Payload
                                                   {
                                                       Weight = relatedInterest.Weight
                                                   }));
                }
            }
        }

        public override Interest SaveOrUpdate(Interest entity)
        {
            Interest retVal = base.SaveOrUpdate(entity);
            var ixEntry = new IndexEntry
            {
                Name = "interests",
                KeyValues = new[]
                {
                    new KeyValuePair<string, object>("name", entity.Name),
                    new KeyValuePair<string, object>("slug", entity.Slug),
                    new KeyValuePair<string, object>("sqlid", retVal.Id)
                }
            };

            var relationships = new List<IRelationshipAllowingParticipantNode<NeoInterest>>
            {
                new InterestBelongsTo(_graphClient.RootNode)
            };

            if (entity.ParentInterest != null)
            {
                Node<NeoInterest> parentNode = FindInterestNodeBySqlId(entity.ParentInterest.Id);
                relationships.Add(
                    new InterestRelatesTo(
                        parentNode.Reference, new InterestRelatesTo.Payload
                        {
                            Weight = InterestRelationshipWeight.Low.ToFloat()
                        }));
            }

            NodeReference<NeoInterest> node = _graphClient.Create(
                new NeoInterest
                {
                    Description = entity.Description,
                    IsMainCategory = entity.IsMainCategory,
                    Name = entity.Name,
                    Slug = entity.Slug,
                    SqlId = retVal.Id
                }, relationships, new[] { ixEntry });
            return retVal;
        }

        #endregion

        private Node<NeoInterest> FindInterestNodeBySqlId(int sqlId)
        {
            const string sqlid = "sqlid";
            return FindInterestNode(sqlid, sqlId.ToString(CultureInfo.InvariantCulture));
        }

        private Node<NeoInterest> FindInterestNodeBySlug(string interestSlug)
        {
            const string slug = "slug";
            return FindInterestNode(slug, interestSlug);
        }

        private Node<NeoInterest> FindInterestNode(string key, string value)
        {
            return _graphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("{0}:{1}", key, value)).First();
        }
    }
}