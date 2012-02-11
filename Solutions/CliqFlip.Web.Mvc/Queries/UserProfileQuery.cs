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

		public UserProfileViewModel GetUser(string username)
		{
			UserProfileViewModel retVal = null;

			User user = Session.Query<User>().FirstOrDefault(x => x.Username == username);


			if (user != null)
			{
				retVal = new UserProfileViewModel
				         	{
				         		Username = user.Username,
				         		Id = user.Id,
				         		Bio = user.Bio,
				         		Headline = user.Headline
				         	};

				List<UserInterestDto> interests =
					user.Interests.Select(interest => new UserInterestDto(interest.Id, interest.Interest.Name.Replace(' ', '\n'), interest.Interest.Slug, null, null, interest.Options.Passion, interest.Options.XAxis, interest.Options.YAxis)).
						ToList();
				retVal.InterestsJson = JsonConvert.SerializeObject(interests);
			}

			return retVal;
		}

		#endregion
	}
}