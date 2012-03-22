using System;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.NHibernate.Extensions;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using NHibernate;
using SharpArch.NHibernate;

namespace CliqFlip.Infrastructure.Repositories
{
	public class UserRepository : LinqRepository<User>, IUserRepository
	{
		#region IUserRepository Members

		public User GetSuggestedUser(User user)
		{
			User retVal;
			int rand = new Random().Next(100);
			bool onlyLocal = rand <= 75;

			IQueryOver<User, User> query = Session.QueryOver<User>()
				.Where(x => x.Id != user.Id);

			if (onlyLocal)
			{
				retVal = query
					.JoinQueryOver(x => x.Location)
					.Where(x => x.MajorLocation.Id == user.Location.MajorLocation.Id)
					.OrderByRandom()
					.Take(1)
					.SingleOrDefault();
			}
			else
			{
				retVal = query
					.OrderByRandom()
					.Take(1)
					.SingleOrDefault();
			}

			return retVal;
		}

		#endregion
	}
}