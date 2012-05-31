using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Migrator.Migrations;
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

		public void GetRelatedInterests(string interestSlug)
		{
			var startingRef = _graphClient.QueryIndex<NeoInterest>("interests", IndexFor.Node, string.Format("slug:{0}", interestSlug)).First();
			var x =
				_graphClient
					.Cypher
					.Start("n", startingRef.Reference)
					.Match("n -[r:INTEREST_RELATES_TO]-(x)")
					.Return<NeoInterestRelatedQuery>("n AS SearchedInterest, r AS FoundInterest")
					.Results;
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
			}, new[] { new InterestBelongsTo(_graphClient.RootNode) }, new[] { ixEntry });
			return retVal;
		}

		#endregion
	}
}