using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest
{
	public class InterestListViewModel
	{
		[Required(ErrorMessage = "Need a search term")]
		public string SearchTearm { get; set; } 

		public IList<InterestListItemViewModel> ListItemViewModels { get; set; }

		#region Nested type: InterestListItemViewModel

		public class InterestListItemViewModel
		{
			public string Name { get; set; }
			public DateTime CreateDate { get; set; }
		}

		#endregion
	}
}