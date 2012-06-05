using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Enums;
using CliqFlip.Domain.Extensions;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;
using Newtonsoft.Json;

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

			var specificInterestItemViewModel = new SpecificInterestGraphViewModel.SpecificInterestItemViewModel
			{
				Id = relatedInterests.OriginalInterest.Id,
				Name = relatedInterests.OriginalInterest.Name,
				Slug = relatedInterests.OriginalInterest.Slug
			};

			var relatedSpecificInterestItemViewModels =
				relatedInterests.WeightedRelatedInterestDtos.Select(x => new SpecificInterestGraphViewModel.RelatedSpecificInterestItemViewModel
				{
					Interest = new SpecificInterestGraphViewModel.SpecificInterestItemViewModel
					{
						Id = x.Interest.Id,
						Name = x.Interest.Name,
						Slug = x.Interest.Slug
					},
					Weight = x.Weight
				}).ToList();

			var interestAndRelatedInterestsToSerialize = new
			{
				MainInterest = specificInterestItemViewModel,
				RelatedInterests = relatedSpecificInterestItemViewModels
			};

			var retVal = new SpecificInterestGraphViewModel
			{
				Interest = specificInterestItemViewModel,
				RelatedInterestItemViewModelsJson = JsonConvert.SerializeObject(interestAndRelatedInterestsToSerialize)
			};

			//TODO:encapsulate this somewhere
			var high = new { Display = "High", Weight = InterestRelationshipWeight.High.ToFloat() };
            var medium = new { Display = "Medium", Weight = InterestRelationshipWeight.Medium.ToFloat() };
            var low = new { Display = "Low", Weight = InterestRelationshipWeight.Low.ToFloat() };

			retVal.RelationShipType = new SelectList(new[] { high, medium, low }, "Weight", "Display");
			return retVal;
		}

		#endregion
	}
}