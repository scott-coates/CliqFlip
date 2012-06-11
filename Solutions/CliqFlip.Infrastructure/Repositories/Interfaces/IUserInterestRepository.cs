using System.Collections.Generic;
using System.Linq;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using SharpArch.Domain.PersistenceSupport;

namespace CliqFlip.Infrastructure.Repositories.Interfaces
{
	public interface IUserInterestRepository : IRepository<UserInterest>
	{
		//userInterestRepo is aggregate root - http://stackoverflow.com/a/5806356/173957
		IList<RankedInterestDto> GetMostPopularInterests();
		IQueryable<UserInterest> GetUserInterestsByInterestTypes(IList<Interest> interests);
	}
}