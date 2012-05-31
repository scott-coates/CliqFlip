using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class InterestListViewModel
	{
		[Required]
		public string SearchKey { get; set; }

		[Required]
		public string AddNewInterstName { get; set; } 

		public IList<InterestListItemViewModel> ListItemViewModels { get; set; }

		#region Nested type: InterestListItemViewModel

		public class InterestListItemViewModel
		{
			public string Name { get; set; }
			public string Slug { get; set; }
			public DateTime CreateDate { get; set; }
		}

		#endregion
	}
}