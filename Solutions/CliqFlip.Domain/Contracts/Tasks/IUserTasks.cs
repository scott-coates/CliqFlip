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

        User Create(UserDto profileToCreate);

		void Login(User user, bool stayLoggedIn);
		bool Login(string username, string password, bool stayLoggedIn);

		User GetUser(string username);

		void SaveProfileImage(User user, HttpPostedFileBase profileImage);
		void SaveWebsite(User user, string siteUrl);
		void Logout(string name);
	}
}
