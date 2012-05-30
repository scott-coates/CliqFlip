using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using NHibernate.Criterion;
using SharpArch.Domain.Specifications;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class InterestRepository : LinqRepository<Interest>, IInterestRepository
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
            return FindAll(new AdHoc<Interest>(x => x.IsMainCategory)).OrderBy(x=>x.Name);
        }

		public IQueryable<Interest> GetAll(int page, string order = "")
		{
			const int pageSize = 10;

			var resultSet = FindAll().Skip((page - 1) * pageSize).Take(pageSize);

			switch(order.ToLower())
			{
				case "createDate asc":
					resultSet = resultSet.OrderBy(x => x.CreateDate);
					break;
				case "createDate desc":
					resultSet = resultSet.OrderByDescending(x => x.CreateDate);
					break;
				case "name asc":
					resultSet = resultSet.OrderBy(x => x.Name);
					break;
				case "name desc":
					resultSet = resultSet.OrderByDescending(x => x.CreateDate);
					break;
			}

			return resultSet;
		}
	}
}