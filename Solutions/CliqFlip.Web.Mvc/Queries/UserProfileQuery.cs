using System.Linq;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.User;
using NHibernate.Linq;
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
				         		Username = user.Username
				         	};

				foreach (UserInterest interest in user.Interests)
				{
					retVal.Interests.Add(new UserProfileViewModel.InterestViewModel
					                     	{
					                     		Name = interest.Interest.Name,
					                     		Passion = interest.Options.Passion,
					                     		XAxis = interest.Options.XAxis,
					                     		YAxis = interest.Options.YAxis
					                     	});
				}
			}

			return retVal;
		}

		#endregion
	}
}