using System;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Logging.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[Authorize(Users = "scott")]
	public class TestController : Controller
	{
		private readonly ILogger _logger;
		private readonly IEmailService _emailService;

		public TestController(ILogger logger, IEmailService emailService)
		{
			_logger = logger;
			_emailService = emailService;
		}

		public ActionResult Index()
		{
			return new EmptyResult();
		}

		public ActionResult Error()
		{
			throw new HttpException(500, "Test Error");
		}

		public ActionResult RandomError()
		{
			throw new HttpException(500, "Test Error " + new Random().NextDouble());
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
		public ActionResult SendEmail(SendEmailViewModel sendEmailViewModel)
		{
			//send it
			_emailService.SendMail(sendEmailViewModel.ToEmailAddress, sendEmailViewModel.Subject, sendEmailViewModel.Body);
			return RedirectToAction("SendEmail");
		}
	}
}