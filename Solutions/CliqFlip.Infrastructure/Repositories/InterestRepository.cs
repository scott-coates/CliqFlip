using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class InterestRepository :LinqRepository<Interest>, IInterestRepository
	{
		public IQueryable<Interest> GetMatchingKeywords(string input)
		{
			var adHoc = new AdHoc<Interest>(s => s.Name.Contains(input) || input.Contains(s.Name));
			return FindAll(adHoc);
		}

		public IQueryable<string> GetSlugAndParentSlug(IList<string> slugs)
		{
			var interestsAndParentQuery = new AdHoc<Interest>(x => slugs.Contains(x.Slug) && x.ParentInterest != null);
			var interestandParents = FindAll(interestsAndParentQuery).Select(x => x.ParentInterest.Slug);

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
            return FindAll(new AdHoc<Interest>(x => x.IsMainCategory));
        }
    }
}