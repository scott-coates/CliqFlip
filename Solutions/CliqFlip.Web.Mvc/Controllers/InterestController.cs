using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class InterestController : Controller
    {
        IInterestTasks _interestTasks;
        public InterestController(IInterestTasks interestTasks)
        {
            _interestTasks = interestTasks;
        }

        //
        // GET: /Interest/
        [OutputCache(Duration=Int32.MaxValue)]
        public ActionResult GetMainCategoryInterests()
        {
            var results = _interestTasks.GetMainCategoryInterests().ToList();
            return View();
        }
    }
}
