using System;
using System.Net;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class ErrorController : Controller
    {
		//http://stackoverflow.com/a/7499406/173957 - seriously, why was everyone else making it so hard?
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
    }
}