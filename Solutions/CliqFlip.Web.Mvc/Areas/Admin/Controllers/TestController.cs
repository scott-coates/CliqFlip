using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.Extensions;
using CliqFlip.Infrastructure.Logging.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.Views.Interfaces;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[FormsAuthReadUserData]
	[Authorize(Roles = "Administrator")]
	public class TestController : Controller
	{
		private readonly ILogger _logger;
		private readonly IEmailService _emailService;
		private readonly IViewRenderer _viewRenderer;
		private readonly IPageParsingService _pageParsingService;
		private readonly IWebContentService _webContentService;


		public TestController(ILogger logger, IEmailService emailService, IViewRenderer viewRenderer, IPageParsingService pageParsingService, IWebContentService webContentService)
		{
			_logger = logger;
			_emailService = emailService;
			_viewRenderer = viewRenderer;
			_pageParsingService = pageParsingService;
			_webContentService = webContentService;
		}

		public ActionResult Index()
		{
			return new EmptyResult();
		}

		public ActionResult Error()
		{
			throw new HttpException((int)HttpStatusCode.InternalServerError, "Test Error");
		}

		public ActionResult RandomError()
		{
			throw new HttpException((int)HttpStatusCode.InternalServerError, "Test Error " + new Random().NextDouble());
		}

		public ActionResult CriticalError()
		{
			throw new CriticalException("CriticalTest " + new Random().NextDouble());
		}

		public ActionResult LogError()
		{
			_logger.LogException(new Exception("Raised Execpton"));
			return new EmptyResult();
		}

		public ActionResult ClientSideError()
		{
			return View();
		}

		[HttpGet]
		public ActionResult SendEmail()
		{
			return View(new SimpleSendEmailViewModel());
		}

		[HttpPost]
		public ActionResult SendEmail(SimpleSendEmailViewModel simpleSendEmailViewModel)
		{
			//TODO - consider using nuget postal, actionmailer, mvc.mailer, or http://razorengine.codeplex.com/
			//TODO - alternate view for plain text
			//send it
			string body = _viewRenderer.RenderView(this, "~/Areas/Admin/Views/TestMailer/TestSimpleSend.cshtml", simpleSendEmailViewModel);
			_emailService.SendMail(simpleSendEmailViewModel.ToEmailAddress, simpleSendEmailViewModel.Subject, body);
			return RedirectToAction("SendEmail");
		}

		public ActionResult ParsePage(string url = null)
		{
			if (!String.IsNullOrWhiteSpace(url))
			{
				var content = _webContentService.GetHtmlFromUrl(url.FormatWebAddress());
				var model = _pageParsingService.GetDetails(content);
				return View(model);
			}
			return View();
		}
	}
}