using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Interest;
using CliqFlip.Web.Mvc.Extensions.Controller;
using CliqFlip.Web.Mvc.Extensions.Exceptions;
using CliqFlip.Web.Mvc.Security.Attributes;
using SharpArch.NHibernate.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[FormsAuthReadUserData(Order = 0)]
	[Authorize(Roles = "Administrator,Manager", Order = 1)]
	public class InterestController : Controller
	{
		private readonly IInterestListQuery _interestListQuery;
		private readonly IInterestTasks _interestTasks;
		private readonly ISpecificInterestGraphQuery _specificInterestGraphQuery;
	    private readonly IHttpContextProvider _httpContextProvider;

		public InterestController(IInterestListQuery interestListQuery, ISpecificInterestGraphQuery specificInterestGraphQuery, IInterestTasks interestTasks, IHttpContextProvider httpContextProvider)
		{
			_interestListQuery = interestListQuery;
			_specificInterestGraphQuery = specificInterestGraphQuery;
			_interestTasks = interestTasks;
		    _httpContextProvider = httpContextProvider;
		}

		[Transaction]
		public ViewResult Index(string searchKey)
		{
			return View(_interestListQuery.GetInterestList(searchKey));
		}

		[Transaction]
		[HttpPost]
		public ActionResult AddInterest(string addNewInterestName)
		{
			if (ModelState.IsValid)
			{
				_interestTasks.Create(addNewInterestName, null);
				this.FlashSuccess(addNewInterestName + " Created");
				return RedirectToAction("Index");
			}

			RouteData.Values["action"] = "Index";
			return Index(null);
		}

        [Transaction]
        [HttpPost]
        public ActionResult UploadInterests(HttpPostedFileBase file)
        {
            //TODO:put this into a viewmodel and check for is valid            
            if (file == null)
            {
                ViewData.ModelState.AddModelError("File", "You need to provide a file first... or don't. Have it your way.");
                RouteData.Values["action"] = "Index";
                return Index(null);
            }

            //set timeout to several minutes because this could take a bit
            _httpContextProvider.Server.ScriptTimeout = 60 /*seconds*/*20 /*minutes*/;

            try
            {
                _interestTasks.UploadInterests(new FileStreamDto(file.InputStream, file.FileName));
            }
            catch (RulesException rex)
            {

                rex.AddModelStateErrors(ModelState);

                RouteData.Values["action"] = "Index";
                return Index(null);
            }
            finally
            {
                file.InputStream.Dispose();
            }
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