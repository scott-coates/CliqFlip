using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface  IUserTasks
	{
		IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases );

        UserDto Create(UserDto profileToCreate);

        bool ValidateUser(string username, string password);

		User GetUser(string username);

		void SaveProfileImage(User user, HttpPostedFileBase profileImage);
		void SaveWebsite(User user, string siteUrl);
	}
}
