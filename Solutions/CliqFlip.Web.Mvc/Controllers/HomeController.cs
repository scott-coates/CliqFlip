using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.ViewModels.Home;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
    	private readonly ISubjectTasks _subjectTasks;

    	public HomeController(ISubjectTasks subjectTasks)
    	{
    		_subjectTasks = subjectTasks;
    	}

    	public ActionResult Index()
    	{
    		var viewModel = new IndexViewModel {SubjectsJson = _subjectTasks.GetSubjectJson()};
    		return View(viewModel);
        }
    }
}
