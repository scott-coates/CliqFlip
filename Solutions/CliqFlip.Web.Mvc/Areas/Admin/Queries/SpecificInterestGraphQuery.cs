using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;

namespace CliqFlip.Web.Mvc.Areas.Admin.Queries
{
	public class SpecificInterestGraphQuery : ISpecificInterestGraphQuery
	{
		private readonly IInterestTasks _interestTasks;

		public SpecificInterestGraphQuery(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		#region ISpecificInterestGraphQuery Members

		public SpecificInterestGraphViewModel GetInterestList(string interest)
		{
			RelatedInterestListDto relatedInterests = _interestTasks.GetRelatedInterests(interest);

			var retVal = new SpecificInterestGraphViewModel
			{
				Interest = new SpecificInterestGraphViewModel.SpecificInterestItemViewModel
				{
					Id = relatedInterests.OriginalInterest.Id,
					Name = relatedInterests.OriginalInterest.Name,
					Slug = relatedInterests.OriginalInterest.Slug
				},
				RelatedInterestItemViewModels = relatedInterests
					.WeightedRelatedInterestDtos
					.Select(x =>
					        new SpecificInterestGraphViewModel.RelatedSpecificInterestItemViewModel
					        {
					        	Interest = new SpecificInterestGraphViewModel.SpecificInterestItemViewModel
					        	{
					        		Id = x.Interest.Id,
					        		Name = x.Interest.Name,
					        		Slug = x.Interest.Slug
					        	},
					        	Wegith = x.Weight
					        }).ToList()
			};

			return retVal;
		}

		#endregion
	}
}