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
		void SaveInterestImage(User user, HttpPostedFileBase profileImage, int userInterestId, string description);
		void SaveWebsite(User user, string siteUrl);
		void Logout(string name);
		void RemoveImage(User user, int imageId);
		void RemoveInterest(User user, int interestId);
		void AddInterestToUser(User user, int interestId);
		void AddInterestsToUser(string name, IEnumerable<UserInterestDto> interestDtos);
        void StartConversation(string starter, string receiver, string text);
        Message ReplyToConversation(int conversationId, string replier, string text);
	}
}
