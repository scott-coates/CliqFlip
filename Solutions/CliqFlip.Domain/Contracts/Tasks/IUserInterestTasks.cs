using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface IUserInterestTasks
	{
        IList<PopularInterestDto> GetMostPopularInterests();
        IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user);
	}
}