using System.Collections.Generic;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class UsersByInterestViewModel
	{
		public IList<IndividualResultViewModel> Results { get; set; }
		public IPagination<IndividualResultViewModel> PagedResults { get; set; }

		public UsersByInterestViewModel()
		{
			Results = new List<IndividualResultViewModel>();
		}

		#region Nested type: IndividualResultViewModel

		public class IndividualResultViewModel
		{
			public string Name { get; set; }
			public IList<IndividualResultInterestViewModel> ResultInterestViewModels { get; set; }
			public string Bio { get; set; }

			public IndividualResultViewModel()
			{
				ResultInterestViewModels = new List<IndividualResultInterestViewModel>();
			}
		}

		#endregion

		public class IndividualResultInterestViewModel
		{
			public string InterestName { get; set; }
			public bool IsMatch { get; set; }
            public float? Passion { get; set; }
        }
	}



}