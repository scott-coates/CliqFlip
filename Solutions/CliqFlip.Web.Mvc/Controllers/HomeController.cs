using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly IInterestTasks _interestTasks;
		private readonly IPrincipal _principal;

		public HomeController(IInterestTasks interestTasks, IPrincipal principal)
		{
			_interestTasks = interestTasks;
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