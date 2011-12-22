using System.Collections.Generic;
using CliqFlip.Domain.Contracts.Queries;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.NHibernate;

namespace CliqFlip.Tasks.Queries
{
	public class InterestJsonQuery : NHibernateQuery<Interest>, IInterestJsonQuery
	{
		public override IList<Interest> ExecuteQuery()
		{
			throw new System.NotImplementedException();
		}

		public string GetInterestJson()
		{
			throw new System.NotImplementedException();
		}
	}
}