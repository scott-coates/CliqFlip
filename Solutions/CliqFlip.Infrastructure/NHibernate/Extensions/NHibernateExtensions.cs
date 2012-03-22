using CliqFlip.Infrastructure.NHibernate.Criteria;
using NHibernate;

namespace CliqFlip.Infrastructure.NHibernate.Extensions
{
	public static class NHibernateExtensions
	{
		//http://puredotnetcoder.blogspot.com/2011/09/nhibernate-queryover-and-newid-or-rand.html
		public static IQueryOver<TRoot, TSubType> OrderByRandom<TRoot, TSubType>(this IQueryOver<TRoot, TSubType> query)
		{
			query.UnderlyingCriteria.AddOrder(new RandomOrder());
			return query;
		}
	}
}