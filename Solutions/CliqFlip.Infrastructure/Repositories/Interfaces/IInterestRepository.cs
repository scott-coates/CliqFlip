using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IInterestRepository : IRepository<Interest>
	{
		// ReSharper disable ReturnTypeCanBeEnumerable.Global
		IQueryable<Interest> GetMatchingKeywords(string input);
        IQueryable<RelatedDistanceInterestDto> GetRelatedInterests(IList<string> slugs);
		Interest GetByName(string name);
        IQueryable<Interest> GetMainCategoryInterests();
		RelatedInterestListDto GetRelatedInterests(string interestSlug);
		void CreateRelationships(RelatedInterestListDto relatedInterestListDto);
		// ReSharper restore ReturnTypeCanBeEnumerable.Global
	}
}