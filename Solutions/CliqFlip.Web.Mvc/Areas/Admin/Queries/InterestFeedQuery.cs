using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Infrastructure.Search;
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

		public InterestListViewModel GetInterestList(string searchTearm)
		{
			var retVal = new InterestListViewModel();
			IList<Interest> interests = _interestTasks.GetAll();

			if (!string.IsNullOrWhiteSpace(searchTearm))
			{
				interests = FuzzySearch
					.GetValuesWithinThresholdLevenshteinDistance(interests,
																 x => x.Name, searchTearm, 3)
					.Select(x => x.Key)
					.ToList();
			}

			retVal.ListItemViewModels = interests.Select(x =>
														 new
															InterestListViewModel.InterestListItemViewModel
														 {
															 Name = x.Name,
															 Slug = x.Slug,
															 CreateDate = x.CreateDate.ToLocalTime()
														 }).ToList();

			return retVal;
		}

		#endregion
	}
}