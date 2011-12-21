using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface  IInterestTasks
	{
		IList<InterestDto> GetInterestDtos();
	}
}
