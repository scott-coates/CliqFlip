using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class InterestSearchViewModel
	{
		public InterestSearchViewModel()
		{
			TagCloudInterests = new List<TagCloudInterestsViewModel>();
		}

		public IList<TagCloudInterestsViewModel> TagCloudInterests { get; set; }

		public class TagCloudInterestsViewModel
		{
			public string Name { get; set; }
			public string Slug { get; set; }
			public int Weight { get; set; }
		}
	}
}