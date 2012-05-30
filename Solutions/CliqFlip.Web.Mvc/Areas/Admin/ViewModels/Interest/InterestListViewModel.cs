using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class InterestListViewModel
	{
		public IList<InterestListItemViewModel> ListItemViewModels { get; set; }

		#region Nested type: InterestListItemViewModel

		public class InterestListItemViewModel
		{
			public string Name { get; set; }
		}

		#endregion
	}
}