using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;

namespace CliqFlip.Web.Mvc.Areas.Admin.Queries
{
	public class InterestListQuery : IInterestListQuery
	{
		private readonly IInterestTasks _interestTasks;

		public InterestListQuery(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		#region IInterestListQuery Members

		public InterestListViewModel GetInterestList(int? page, string orderBy = "createDate desc")
		{
			var retVal = new InterestListViewModel();
			IList<Interest> interests = _interestTasks.GetAll(page ?? 1);
			retVal.ListItemViewModels = interests.Select(x => new
			                                                  	InterestListViewModel.InterestListItemViewModel
			{
				Name = x.Name
			}).ToList();

			return retVal;
		}

		#endregion
	}
}