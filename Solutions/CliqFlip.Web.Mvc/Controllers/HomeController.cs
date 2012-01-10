using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISubjectTasks _subjectTasks;

		public HomeController(ISubjectTasks subjectTasks)
		{
			_subjectTasks = subjectTasks;
		}

		public ActionResult Index()
		{
			var viewModel = new IndexViewModel
			                	{
			                		KeywordSearchUrl = "\"" + Url.Action("KeywordSearch", "Interest") + "\""
			                	};
			return View(viewModel);
		}
	}
}