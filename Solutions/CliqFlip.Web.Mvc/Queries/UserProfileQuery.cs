using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.User;
using NHibernate.Linq;
using Newtonsoft.Json;
using SharpArch.NHibernate;

namespace CliqFlip.Web.Mvc.Queries
{
	public class UserProfileQuery : NHibernateQuery, IUserProfileQuery
	{
		#region IUserProfileQuery Members

		public UserProfileIndexViewModel GetUserProfileIndex(string username, IPrincipal requestingUser)
		{
			UserProfileIndexViewModel retVal = null;

			User user = GetUser(username);


			if (user != null)
			{
				retVal = new UserProfileIndexViewModel
				{
					Bio = user.Bio,
					TwitterUsername = user.TwitterUsername,
					YouTubeUsername = user.YouTubeUsername,
					WebsiteUrl = user.UserWebsite.SiteUrl,
					FacebookUsername = user.FacebookUsername
				};

				FillBaseProperties(retVal, user, requestingUser);

				List<UserInterestDto> interests =
					user.Interests.Select(interest => new UserInterestDto(interest.Id, interest.Interest.Name.Replace(' ', '\n'), interest.Interest.Slug, null, null, interest.Options.Passion, interest.Options.XAxis, interest.Options.YAxis)).
						ToList();

				retVal.InterestsJson = JsonConvert.SerializeObject(interests);
			}

			return retVal;
		}

		public UserSocialMediaViewModel GetUserSocialMedia(string username, IPrincipal requestingUser)
		{
			UserSocialMediaViewModel retVal = null;

			User user = GetUser(username);


			if (user != null)
			{
				retVal = new UserSocialMediaViewModel
				{
					TwitterUsername = user.TwitterUsername,
					YouTubeUsername = user.YouTubeUsername,
					FacebookUsername = user.FacebookUsername,
					WebsiteFeedUrl = user.UserWebsite.FeedUrl
				};

				FillBaseProperties(retVal, user, requestingUser);
			}

			return retVal;
		}

		public UserInterestsViewModel GetUserIntersets(string username, IPrincipal requestingUser)
		{
			UserInterestsViewModel retVal = null;

			User user = GetUser(username);


			if (user != null)
			{
				retVal = new UserInterestsViewModel();

				FillBaseProperties(retVal, user, requestingUser);

				User visitor = null;

				if (retVal.AuthenticatedVisitor)
				{
					visitor = GetUser(requestingUser.Identity.Name);
				}

				foreach (UserInterest interest in user.Interests)
				{
					var interestViewModel = new UserInterestsViewModel.InterestViewModel
					{
						Name = interest.Interest.Name,
						UserInterestId = interest.Id,
						InterestId = interest.Interest.Id,
						VisitorSharesThisInterest = visitor != null && visitor.Interests.Any(x => x.Interest == interest.Interest),
						Images = interest
							.Images
							.Select(x =>
									new UserInterestsViewModel.InterestImageViewModel(x)).ToList()
					};

					retVal.Interests.Add(interestViewModel);
				}
			}

			return retVal;
		}

        public UserInboxViewModel GetUsersInbox(IPrincipal requestingUser)
        {
            UserInboxViewModel retVal = null;
            User user = Session.Query<User>().FirstOrDefault(x => x.Username == requestingUser.Identity.Name);

            var activeConversations = user.Participants.Where(participant => participant.IsActive).Select(participant => participant.Conversation).ToList();

            if (user != null)
            {
                retVal = new UserInboxViewModel();
                foreach (var conversation in activeConversations)
                {
                    var sender = conversation.Participants.Where(x => x.User != user).Single().User;
                    UserInboxViewModel.ConversationViewModel conv = new UserInboxViewModel.ConversationViewModel
                    {
                        Id = conversation.Id,
                        HasUnreadMessages = conversation.HasNewMessagesFor(user),
                        SenderImage = sender.ProfileImage != null ? sender.ProfileImage.Data.ThumbFileName : "/Content/img/empty-avatar.jpg",
                        Sender = sender.Username,
                        LastMessage = conversation.Messages.OrderByDescending(message => message.SendDate).First().Text
                    };
                    retVal.Conversations.Add(conv);
                }
                FillBaseProperties(retVal, user, requestingUser);
            }
            
            return retVal;
        }

		#endregion

		private User GetUser(string username)
		{
			return Session.Query<User>().FirstOrDefault(x => x.Username == username);
		}

		private void FillBaseProperties(UserProfileViewModel retVal, User user, IPrincipal requestingUser)
		{
			retVal.Id = user.Id;
			retVal.Headline = user.Headline;
			retVal.Username = user.Username;
			if (user.Username == requestingUser.Identity.Name)
			{
				retVal.AuthenticatedProfileOwner = true;
			}
			else if (requestingUser.Identity.IsAuthenticated)
			{
				retVal.AuthenticatedVisitor = true;
			}
			if (user.ProfileImage != null)
			{
				retVal.ProfileImageUrl = user.ProfileImage.Data.MediumFileName;
			}
		}
	}
}