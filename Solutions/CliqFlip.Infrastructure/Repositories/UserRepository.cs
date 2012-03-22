using System;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.NHibernate.Extensions;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class UserRepository : LinqRepository<User>, IUserRepository
	{
		#region IUserRepository Members

		public User GetSuggestedUser(User user)
		{
			int rand = new Random().Next(100);
			bool onlyLocal = rand < 75;

			return Session.QueryOver<User>()
				.Where(x => (onlyLocal || x.Location.MajorLocation == user.Location.MajorLocation) && x != user)
				.OrderByRandom()
				.SingleOrDefault();
		}

		#endregion
	}
}