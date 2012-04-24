using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.Logging.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.Views.Interfaces;
using HtmlAgilityPack;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
    [FormsAuthReadUserData]
    //[Authorize(Roles = "Administrator")]
    public class PageParsingTestController : Controller
    {
        private readonly IPageParsingService _pageParsingService;
        private readonly IHtmlService _htmlService;

        public PageParsingTestController(IPageParsingService pageParsingService, IHtmlService htmlService)
        {
            _pageParsingService = pageParsingService;
            _htmlService = htmlService;
        }

        public ActionResult Index(string url = null)
        {
            if (!String.IsNullOrWhiteSpace(url))
            {
                var content = _htmlService.GetHtmlFromUrl(url);
                var model = _pageParsingService.GetDetails(content);
                return View(model);
            }
            return View();
        }
    }
}