using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class UserInterestRepository : LinqRepository<UserInterest>, IUserInterestRepository
	{
		#region IUserInterestRepository Members

		public IList<RankedInterestDto> GetMostPopularInterests()
		{
			var popularInterests =
				FindAll().ToList()
					.GroupBy(x => x.Interest)
					.Select(x => new {x.Key, Count = x.Count()})
					.OrderByDescending(x => x.Count)
					.Take(10).ToList();

			return popularInterests.Select(x => new RankedInterestDto(x.Key.Id, x.Key.Name, x.Key.Slug, x.Count)).ToList();
		}

		#endregion
	}
}