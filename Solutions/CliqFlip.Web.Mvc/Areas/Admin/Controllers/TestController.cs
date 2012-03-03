using System;
using System.Web;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[Authorize(Users = "scott")]
	public class TestController : Controller
	{
		public ActionResult Index()
		{
			return new EmptyResult();
		}

		public ActionResult Error()
		{
			throw new HttpException(500,"Test Error");
		}

		public ActionResult RandomError()
		{
			throw new HttpException(500, "Test Error " + new Random().NextDouble());
		}
	}
}