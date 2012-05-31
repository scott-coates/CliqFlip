using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IInterestRepository : IRepository<Interest>
	{
		// ReSharper disable ReturnTypeCanBeEnumerable.Global
		IQueryable<Interest> GetMatchingKeywords(string input);
		IQueryable<string> GetSlugAndParentSlug(IList<string> slugs);
		Interest GetByName(string name);
        IQueryable<Interest> GetMainCategoryInterests();
		// ReSharper restore ReturnTypeCanBeEnumerable.Global

    }
}