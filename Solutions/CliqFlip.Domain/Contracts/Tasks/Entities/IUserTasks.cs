﻿using System.Collections.Generic;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;

namespace CliqFlip.Domain.Contracts.Tasks.Entities
{
	public interface  IUserTasks
	{
		// ReSharper disable ReturnTypeCanBeEnumerable.Global
		User Create(UserCreateDto profileToCreate, LocationData location);

		void Login(User user, bool stayLoggedIn);
		bool Login(string username, string password, bool stayLoggedIn);

		User GetUser(string username);
		User GetSuggestedUser(string username);

		void SaveProfileImage(User user, FileStreamDto profileImage);
        void PostImage(User user, FileStreamDto profileImage, int userInterestId, string description);
        void PostImage(User user, int userInterestId, string description, string imageUrl);
        void PostVideo(User user, int userInterestId, string videoUrl);
        void PostWebPage(User user, int userInterestId, string linkUrl);
        void PostStatus(User user, int userInterestId, string description);
		void SaveWebsite(User user, string siteUrl);
		void SavePassword(User user, string password);
		void SaveLocation(User user, LocationData locationData);

		void Logout(string name);
		void RemovePost(User user, int postId);
		void RemoveInterest(User user, int interestId);
		void AddInterestToUser(User user, int interestId);
		void AddInterestsToUser(string name, IEnumerable<UserAddInterestDto> interestDtos);
		void StartConversation(string starter, string receiver, string messageText, string subject, string body);
		Message ReplyToConversation(Conversation conversation, User sender,User receiver, string messageText,string subject, string body);

		bool IsUsernameOrEmailAvailable(string value);
		// ReSharper restore ReturnTypeCanBeEnumerable.Global
	}
}
