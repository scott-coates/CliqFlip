using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.ViewModels.Search;
using CliqFlip.Web.Mvc.ViewModels.User;
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
        private readonly IInterestFeedQuery _interestFeedQuery;
        private readonly IPrincipal _principal;

        public SearchController(IUsersByInterestsQuery usersByInterestsQuery,
                                IInterestTasks interestTasks,
                                IInterestFeedQuery interestFeedQuery,
                                IPrincipal principal)
        {
            _usersByInterestsQuery = usersByInterestsQuery;
            _interestTasks = interestTasks;
            _interestFeedQuery = interestFeedQuery;
            _principal = principal;
        }

        [Transaction]
        [Authorize]
        public JsonNetResult InterestFeed(int? page)
        {
            var viewModel = _interestFeedQuery.GetUsersByInterests(_principal.Identity.Name, page, Url);
            return new JsonNetResult(viewModel.Posts);
        }

        [Authorize]
        [Transaction]
        [Authorize]
        public ActionResult Index(string q, int? page)
        {
            var viewModel = _usersByInterestsQuery.GetGetUsersByInterests(q, page, _principal.Identity.Name);

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
                    .Select(
                        x => new InterestSearchViewModel.TagCloudInterestsViewModel

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