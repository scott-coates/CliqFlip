using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;

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
    		var viewModel = new IndexViewModel();
    		viewModel.InterestDtos = _interestTasks.GetInterestDtos();
            return View(viewModel);
        }

    }
}
