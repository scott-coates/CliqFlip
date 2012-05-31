using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class SpecificInterestGraphViewModel
	{
		public SpecificInterestItemViewModel Interest { get; set; }

		public List<RelatedSpecificInterestItemViewModel> RelatedInterestItemViewModels { get; set; }

		#region Nested type: RelatedSpecificInterestItemViewModel

		public class RelatedSpecificInterestItemViewModel
		{
			public SpecificInterestItemViewModel Interest { get; set; }
			public float Wegith { get; set; }
		}

		#endregion

		#region Nested type: SpecificInterestItemViewModel

		public class SpecificInterestItemViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Slug { get; set; }
		}

		#endregion
	}
}