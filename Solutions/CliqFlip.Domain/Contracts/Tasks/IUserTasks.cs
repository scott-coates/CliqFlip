using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface  IUserTasks
	{
		IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases );

        UserDto Create(UserDto profileToCreate);

        bool ValidateUser(string username, string password);
		void UpdateMindMap(int userId, IEnumerable<UserInterestDto> userInterests);
        void UpdateHeadline(string headline);
        void UpdateBio(string bio);
	}
}
