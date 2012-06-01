using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;
using CliqFlip.Web.Mvc.Extensions.Controller;
using CliqFlip.Web.Mvc.Security.Attributes;
using SharpArch.NHibernate.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[FormsAuthReadUserData(Order = 0)]
	[Authorize(Roles = "Administrator,Management", Order = 1)]
	public class InterestController : Controller
	{
		private readonly IInterestListQuery _interestListQuery;
		private readonly IInterestTasks _interestTasks;
		private readonly ISpecificInterestGraphQuery _specificInterestGraphQuery;

		public InterestController(IInterestListQuery interestListQuery, ISpecificInterestGraphQuery specificInterestGraphQuery, IInterestTasks interestTasks)
		{
			_interestListQuery = interestListQuery;
			_specificInterestGraphQuery = specificInterestGraphQuery;
			_interestTasks = interestTasks;
		}

		[Transaction]
		public ViewResult Index(string searchKey)
		{
			return View(_interestListQuery.GetInterestList(searchKey));
		}

		[Transaction]
		[HttpPost]
		public ActionResult AddInterest(string addNewInterstName)
		{
			if (ModelState.IsValid)
			{
				_interestTasks.Create(addNewInterstName, null);
				this.FlashSuccess(addNewInterstName + " Created");
				return RedirectToAction("Index");
			}

			RouteData.Values["action"] = "Index";
			return Index(null);
		}

		[Transaction]
		public ViewResult SpecificInterest(string id)
		{
			//TODO: look into mvc restful routing
			//https://github.com/mccalltd/AttributeRouting/wiki/Routing-to-Actions
			//http://stevehodgkiss.github.com/restful-routing/
			//haacked..but it looks lame?
			return View("Interest", _specificInterestGraphQuery.GetInterestList(id));
		}

		[Transaction]
		[HttpPost]
		public ActionResult AddInterestRelationship(CreateInterestRelationshipViewModel createInterestRelationshipViewModel)
		{
			Interest mainInterest = _interestTasks.Get(createInterestRelationshipViewModel.Id);

			if (ModelState.IsValid)
			{
				var relatedInterestListDto = new RelatedInterestListDto
				{
					OriginalInterest = new RelatedInterestListDto.RelatedInterestDto
					{
						Id = mainInterest.Id,
						Name = mainInterest.Name,
						Slug = mainInterest.Slug
					},
					WeightedRelatedInterestDtos = createInterestRelationshipViewModel
						.UserInterests
						.Select(x => new RelatedInterestListDto.WeightedRelatedInterestDto
						{
							Weight = createInterestRelationshipViewModel.RelationShipType,
							Interest = new RelatedInterestListDto.RelatedInterestDto
							{
								Id = x.Id,
								Name = x.Name,
								ParentId = x.CategoryId
							}
						}).ToList()
				};


				_interestTasks.CreateRelationships(relatedInterestListDto);
				this.FlashSuccess("Updated Relationships");
				return RedirectToAction("SpecificInterest", new {Id = mainInterest.Slug});
			}

			RouteData.Values["action"] = "SpecificInterest";
			return SpecificInterest(mainInterest.Slug);
		}
	}
}