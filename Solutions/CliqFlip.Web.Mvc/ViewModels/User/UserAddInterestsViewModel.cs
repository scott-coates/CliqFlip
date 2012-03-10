using System.Collections.Generic;
using CliqFlip.Web.Mvc.Validation;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserAddInterestsViewModel
	{
		public UserAddInterestsViewModel()
		{
			UserInterests = new List<InterestCreate>();
		}

		[CollectionNotEmptyAttribute(ErrorMessage = "Okay, we get it, you're not very interesting. But please, for our sake, just provide us with an interest that tells us about yourself.")]
		public List<InterestCreate> UserInterests { get; set; }
	}
}