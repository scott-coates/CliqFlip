using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Neo.NodeTypes;
using CliqFlip.Infrastructure.Neo.Relationships;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using Neo4jClient;
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

		public IQueryable<string> GetSlugAndParentSlug(IList<string> slugs)
		{
			var interestsAndParentQuery = new AdHoc<Interest>(x => slugs.Contains(x.Slug) && x.ParentInterest != null);
			IQueryable<string> interestandParents = FindAll(interestsAndParentQuery).Select(x => x.ParentInterest.Slug);

			return interestandParents;
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

			Func<NeoInterest, RelatedInterestListDto.RelatedInterestDto> convert = x => new RelatedInterestListDto.RelatedInterestDto
			{
				Id = x.SqlId,
				Name = x.Name,
				Slug = x.Slug
			};

			retVal.OriginalInterest = convert(startingRef.Data);

			IEnumerable<NeoInterestRelatedQuery> neoRelatedInterestQuery =
				_graphClient
					.Cypher
					.Start("n", startingRef.Reference)
					.Match("n -[r:INTEREST_RELATES_TO]-(x)")
					.Return<NeoInterestRelatedQuery>("x AS FoundInterest, r.Weight AS Weight")
					.Results;

			if (neoRelatedInterestQuery != null)
			{
				retVal.WeightedRelatedInterestDtos = neoRelatedInterestQuery.Select(x => new RelatedInterestListDto.WeightedRelatedInterestDto
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
			foreach (RelatedInterestListDto.WeightedRelatedInterestDto relatedInterest in relatedInterestListDto.WeightedRelatedInterestDtos)
			{
				Node<NeoInterest> relatedNode = FindInterestNodeBySqlId(relatedInterest.Interest.Id);
				_graphClient.CreateRelationship(startingRef.Reference, new InterestRelatesTo(relatedNode.Reference, new InterestRelatesTo.Payload { Weight = relatedInterest.Weight }));
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

			NodeReference<NeoInterest> node = _graphClient.Create(new NeoInterest
			{
				Description = entity.Description,
				IsMainCategory = entity.IsMainCategory,
				Name = entity.Name,
				Slug = entity.Slug,
				SqlId = retVal.Id
			}, new[] {new InterestBelongsTo(_graphClient.RootNode)}, new[] {ixEntry});
			return retVal;
		}

		#endregion

		private Node<NeoInterest> FindInterestNodeBySqlId(int sqlId)
		{
			const string slug = "sqlid";
			return FindInterestNode(sqlId.ToString(CultureInfo.InvariantCulture), slug);
		}

		private Node<NeoInterest> FindInterestNodeBySlug(string interestSlug)
		{
			const string slug = "slug";
			return FindInterestNode(interestSlug, slug);
		}

		private Node<NeoInterest> FindInterestNode(string interestSlug, string key)
		{
			return _graphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("{0}:{1}", key, interestSlug)).First();
		}
	}
}