using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.ViewModels.Popup
{
	public class BookmarkMediumViewModel
	{
		public string MediumUrl { get; set; }

		public List<InterestsViewModel> Interests { get; set; }

		public BookmarkMediumViewModel()
		{
			Interests = new List<InterestsViewModel>();
		}

		public SelectList InterestList
		{
			get
			{
				return new SelectList(Interests, "UserInterestId", "InterestName");
			}
		}

		public class InterestsViewModel
		{
			public string InterestName { get; set; }
			public int UserInterestId { get; set; }
		}
	}
}