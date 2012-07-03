using System.Collections.Generic;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.ViewModels.Popup
{
	public class CreatePostViewModel
	{
		public string MediumUrl { get; set; }

		public List<InterestsViewModel> Interests { get; set; }

		public SelectList InterestList
		{
			get { return new SelectList(Interests, "UserInterestId", "InterestName"); }
		}

		public string Username { get; set; }

		public CreatePostViewModel()
		{
			Interests = new List<InterestsViewModel>();
		}

		#region Nested type: InterestsViewModel

		public class InterestsViewModel
		{
			public string InterestName { get; set; }
			public int UserInterestId { get; set; }
		}

		#endregion
	}
}