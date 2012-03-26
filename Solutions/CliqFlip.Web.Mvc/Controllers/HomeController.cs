using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPrincipal _principal;

		public HomeController(IPrincipal principal)
		{
			_principal = principal;
		}

		public ActionResult Index()
		{
			if (_principal.Identity.IsAuthenticated)
			{
				return Redirect("~/u");
			}

			return View();
		}
	}
}