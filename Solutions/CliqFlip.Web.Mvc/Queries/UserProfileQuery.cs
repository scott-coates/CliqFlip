using System.Collections.Generic;
using System.Linq;
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

		public UserProfileIndexViewModel GetUserProfileIndex(string username)
		{
			UserProfileIndexViewModel retVal = null;

			User user = Session.Query<User>().FirstOrDefault(x => x.Username == username);


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

				FillBaseProperties(retVal, user);

				List<UserInterestDto> interests =
					user.Interests.Select(interest => new UserInterestDto(interest.Id, interest.Interest.Name.Replace(' ', '\n'), interest.Interest.Slug, null, null, interest.Options.Passion, interest.Options.XAxis, interest.Options.YAxis)).
						ToList();

				retVal.InterestsJson = JsonConvert.SerializeObject(interests);
			}

			return retVal;
		}

		public UserSocialMediaViewModel GetUserSocialMedia(string username)
		{
			UserSocialMediaViewModel retVal = null;

			User user = Session.Query<User>().FirstOrDefault(x => x.Username == username);


			if (user != null)
			{
				retVal = new UserSocialMediaViewModel
				{
					TwitterUsername = user.TwitterUsername,
					YouTubeUsername = user.YouTubeUsername,
					FacebookUsername = user.FacebookUsername,
					WebsiteFeedUrl = user.UserWebsite.FeedUrl
				};

				FillBaseProperties(retVal, user);
			}

			return retVal;
		}

		public UserInterestsViewModel GetUserIntersets(string username)
		{
			UserInterestsViewModel retVal = null;

			User user = Session.Query<User>().FirstOrDefault(x => x.Username == username);

			if (user != null)
			{
				retVal = new UserInterestsViewModel();
				foreach (UserInterest interest in user.Interests)
				{
					var interestViewModel = new UserInterestsViewModel.InterestViewModel
					{
						Name = interest.Interest.Name,
						UserInterestId = interest.Id,
						Images = interest
							.Images
							.Select(x =>
							        new UserInterestsViewModel.InterestImageViewModel(x)).ToList()
					};

					retVal.Interests.Add(interestViewModel);
				}

				FillBaseProperties(retVal, user);
			}

			return retVal;
		}

		#endregion

		private void FillBaseProperties(UserProfileViewModel retVal, User user)
		{
			retVal.Id = user.Id;
			retVal.Headline = user.Headline;
			retVal.Username = user.Username;
			retVal.ProfileImageUrl = user.ProfileImage.Data.MediumFileName;
		}
	}
}