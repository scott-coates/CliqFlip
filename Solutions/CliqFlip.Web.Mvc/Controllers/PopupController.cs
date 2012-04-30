using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class PopupController : Controller
	{
		[Authorize]
		public ActionResult BookmarkMedium()
		{
			return View();
		}
	}
}