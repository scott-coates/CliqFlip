using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;

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
			else
			{
				var viewModel = new IndexViewModel
				{
					//TODO: Put this in a query - like how we do with the conversation controller
					KeywordSearchUrl = "\"" + Url.Action("Interest", "Search") + "\"",
					TagCloudInterests = _interestTasks
						.GetMostPopularInterests()
						.OrderBy(x => x.Name)
						.Select(x => new IndexViewModel.TagCloudInterestsViewModel

						{
							Name = x.Name,
							Slug = x.Slug,
							Weight = x.Count
						}).ToList()
				};

				return View(viewModel);
			}
		}
	}
}