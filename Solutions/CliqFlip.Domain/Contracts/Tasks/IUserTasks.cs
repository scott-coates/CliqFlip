using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface  IUserTasks
	{
		IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IEnumerable<int> interestIds );
	}
}
