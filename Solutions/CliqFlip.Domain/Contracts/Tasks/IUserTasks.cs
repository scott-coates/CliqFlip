using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Tasks
{
	public interface  IUserTasks
	{
		// ReSharper disable ReturnTypeCanBeEnumerable.Global
		IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases );

		User Create(UserDto profileToCreate, LocationData location);

		void Login(User user, bool stayLoggedIn);
		bool Login(string username, string password, bool stayLoggedIn);

		User GetUser(string username);
		User GetSuggestedUser(string username);

		void SaveProfileImage(User user, FileStreamDto profileImage);
		void SaveInterestImage(User user, FileStreamDto profileImage, int userInterestId, string description);
		void SaveInterestImage(User user, int userInterestId, string description, string imageUrl);
		void SaveInterestVideo(User user, int userInterestId, string videoUrl);
		void SaveInterestWebPage(User user, int userInterestId, string linkUrl);
		void SaveWebsite(User user, string siteUrl);
		void Logout(string name);
		void RemoveMedium(User user, int mediumId);
		void RemoveInterest(User user, int interestId);
		void AddInterestToUser(User user, int interestId);
		void AddInterestsToUser(string name, IEnumerable<UserInterestDto> interestDtos);
		void StartConversation(string starter, string receiver, string messageText, string subject, string body);
		Message ReplyToConversation(Conversation conversation, User sender,User receiver, string messageText,string subject, string body);

		bool IsUsernameOrEmailAvailable(string value);
		// ReSharper restore ReturnTypeCanBeEnumerable.Global
	}
}
