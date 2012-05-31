using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class SpecificInterestGraphViewModel
	{
		public string Name { get; set; }
		public List<SpecificInterestItemViewModel> SpecificInterestItemViewModels { get; set; } 

		public class SpecificInterestItemViewModel
		{
			public string Node1 { get; set; }
			public string Node2 { get; set; }
		}
	}
}