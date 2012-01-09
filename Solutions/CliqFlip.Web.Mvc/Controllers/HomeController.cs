using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
    	private readonly IInterestTasks _interestTasks;

    	public HomeController(IInterestTasks interestTasks)
    	{
    		_interestTasks = interestTasks;
    	}

    	public ActionResult Index()
    	{
    		var viewModel = new IndexViewModel {InterestsJson = _interestTasks.GetInterestJson()};
    		return View(viewModel);
        }
    }
}
