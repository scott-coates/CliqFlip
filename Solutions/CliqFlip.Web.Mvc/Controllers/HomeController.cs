using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly IInterestTasks _interestTasks;

		public HomeController(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		public ActionResult Index()
		{
			var viewModel = new IndexViewModel
			                	{
			                		KeywordSearchUrl = "\"" + Url.Action("Interest", "Search") + "\"",
			                		TagCloudInterests = _interestTasks
			                			.GetMostPopularInterests()
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