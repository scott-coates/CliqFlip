using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace CliqFlip.Infrastructure.NHibernate.Criteria
{
	//http://puredotnetcoder.blogspot.com/2011/09/nhibernate-queryover-and-newid-or-rand.html
	public class RandomOrder : Order
	{
		public RandomOrder() : base("", true)
		{
		}

		public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return new SqlString("NEWID()");
		}
	}
}