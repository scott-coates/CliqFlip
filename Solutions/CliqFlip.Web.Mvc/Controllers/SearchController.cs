using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;
using CliqFlip.Web.Mvc.ViewModels.Search;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
	using System.Web.Mvc;

	public class SearchController : Controller
	{
		private readonly IInterestTasks _interestTasks;

		public SearchController(IInterestTasks interestTasks)
		{
			_interestTasks = interestTasks;
		}

		public ActionResult Index(string as_values_post)
		{
			//TODO: Look into using something like this for posted objects - http://stackoverflow.com/questions/4316301/asp-net-mvc-2-bind-a-models-property-to-a-different-named-value
			return new EmptyResult();
		}
	}
}