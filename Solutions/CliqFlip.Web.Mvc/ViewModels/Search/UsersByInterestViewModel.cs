using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class UsersByInterestViewModel
	{
		public IList<IndividualResultViewModel> Results { get; set; }

		public UsersByInterestViewModel()
		{
			Results = new List<IndividualResultViewModel>();
		}
	}

	public class IndividualResultViewModel
	{
		public string Name { get; set; }
		public string Interests { get; set; }
		public string Bio { get; set; }
	}
}