using CliqFlip.Domain.Contracts.Tasks;

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

            return View();
        }

    }
}
