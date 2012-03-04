using System.Net;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.Logging.Interfaces;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class ErrorController : Controller
	{
		private readonly ILogger _logger;

		public ErrorController(ILogger logger)
		{
			_logger = logger;
		}

		//http://stackoverflow.com/a/7499406/173957 - seriously, why was everyone else making it so hard?
		public ActionResult NotFound()
		{
			Response.StatusCode = (int) HttpStatusCode.NotFound;
			return View();
		}

		public ActionResult ServerError()
		{
			Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			return View();
		}

		public ActionResult LogJavaScriptError(string message)
		{
			//http://joel.net/logging-errors-with-elmah-in-asp.net-mvc-3--part-5--javascript
			_logger.LogException(new JavaScriptException(message));
			return new EmptyResult();
		}
	}
}