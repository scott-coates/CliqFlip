using System.Collections.Generic;
using CliqFlip.Domain.Dtos.UserInterest;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface IUserInterestTasks
	{
        IList<PopularInterestDto> GetMostPopularInterests();
        IList<InterestInCommonDto> GetInterestsInCommon(User viewingUser, User user);
	    void SaveUserInterest(UserInterest userInterest);
	    void DeleteUserInterest(UserInterest userInterest);
	}
}