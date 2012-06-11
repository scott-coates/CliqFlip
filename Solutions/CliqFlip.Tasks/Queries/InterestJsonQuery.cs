using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Queries;

using CliqFlip.Domain.Entities;
using NHibernate.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.Queries
{
	public class InterestJsonQuery : NHibernateQuery<User>, IUsersByInterestsQuery
	{
		public override IList<User> ExecuteQuery()
		{
			return null;
		}
	}
}