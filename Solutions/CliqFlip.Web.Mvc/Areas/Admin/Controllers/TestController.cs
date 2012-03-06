using System;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Logging.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using CliqFlip.Web.Mvc.Mailers;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[Authorize(Users = "scott")]
	public class TestController : Controller
	{
		private readonly ILogger _logger;
		private readonly IEmailService _emailService;
		private readonly ITestMailer _testMailer;

		public TestController(ILogger logger, IEmailService emailService, ITestMailer testMailer)
		{
			_logger = logger;
			_emailService = emailService;
			_testMailer = testMailer;
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
			return View();
		}

		[HttpPost]
		public ActionResult SendEmail(SimpleSendEmailViewModel simpleSendEmailViewModel)
		{
			using(var message = _testMailer.TestSimpleSend(simpleSendEmailViewModel))
			{
				_emailService.SendMail(simpleSendEmailViewModel.ToEmailAddress, simpleSendEmailViewModel.Subject, message.Body);
			}

			return RedirectToAction("SendEmail");
		}
	}
}