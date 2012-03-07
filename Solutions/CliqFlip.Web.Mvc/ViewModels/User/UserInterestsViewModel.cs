using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserInterestsViewModel : UserProfileViewModel
	{
		public IList<InterestViewModel> Interests { get; set; }
		public bool CanEdit { get; set; }

		public UserInterestsViewModel()
		{
			Interests = new List<InterestViewModel>();
		}

		#region Nested type: TagCloudInterestsViewModel

		public class InterestViewModel
		{
			public string Name { get; set; }
			public int UserInterestId { get; set; }
		}

		#endregion
	}
}