using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var viewModel = new IndexViewModel
								{
									KeywordSearchUrl = "\"" + Url.Action("Interest", "Search") + "\""
								};
			return View(viewModel);
		}
	}
}