using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.ViewModels.Search;
using Newtonsoft.Json;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;
using MvcContrib.Pagination;

namespace CliqFlip.Web.Mvc.Controllers
{
	using System.Web.Mvc;

	public class SearchController : Controller
	{
		private readonly IUsersByInterestsQuery _usersByInterestsQuery;
		private readonly IInterestTasks _interestTasks;

		public SearchController(IUsersByInterestsQuery usersByInterestsQuery, IInterestTasks interestTasks)
		{
			_usersByInterestsQuery = usersByInterestsQuery;
			_interestTasks = interestTasks;
		}

		[HttpPost]
		public JsonNetResult InterestFeed(int start, int limit)
		{
			var rows = Enumerable.Range(1, 10).Select(x => new { Name = "x" + x });
			var result = new { success = true, total = 30, data = rows, message = string.Empty };
			Thread.Sleep(1000);
			return new JsonNetResult(result);
		}

		[Transaction]
		public ActionResult Index(string q, int? page)
		{
			var viewModel = _usersByInterestsQuery.GetGetUsersByInterests(q, page);

			return View(viewModel);
		}

		[Transaction]
		[AllowAnonymous]
		public ActionResult Interest(string input)
		{
			//TODO: put this in a view model query
			IList<InterestKeywordDto> matchingKeywords = _interestTasks.GetMatchingKeywords(input);

			if (!matchingKeywords.Any(x => x.Name.ToLower() == input.ToLower()))
			{
				string formattedName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
				matchingKeywords.Insert(0, new InterestKeywordDto { Name = formattedName, Slug = "-1" + input.ToLower() });
			}

			var retVal = new JsonNetResult(matchingKeywords)
			{
				SerializerSettings =
				{
					NullValueHandling = NullValueHandling.Include
				}
			};

			return retVal;
		}

		[OutputCache(Duration = Int32.MaxValue)]
		[Transaction]
		[ChildActionOnly]
		[AllowAnonymous]
		public ActionResult InterestSearch()
		{
			var viewModel = new InterestSearchViewModel
			{
				//TODO: Put this in a query - like how we do with the conversation controller
				TagCloudInterests = _interestTasks
					.GetMostPopularInterests()
					.OrderBy(x => x.Name)
					.Select(x => new InterestSearchViewModel.TagCloudInterestsViewModel

					{
						Name = x.Name,
						Slug = x.Slug,
						Weight = x.Count
					}).ToList()
			};

			return PartialView(viewModel);
		}
	}
}