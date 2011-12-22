using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Domain.Contracts.Queries
{
	public interface IInterestJsonQuery : IQuery<Interest>
	{
		string GetInterestJson();
	}
}
