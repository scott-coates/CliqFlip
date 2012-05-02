using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using SharpArch.Web.Mvc.JsonNet;
using CliqFlip.Web.Mvc.Security.Attributes;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class InterestController : Controller
    {
        private readonly IInterestTasks _interestTasks;
        public InterestController(IInterestTasks interestTasks)
        {
            _interestTasks = interestTasks;
        }

        //
        // GET: /Interest/GetMainCategoryInterests
        // This action will be used by the module used to add interests
        //
        // cache this as long as possible
        // the list most likely wont change frequently
		[OutputCache(Duration = Int32.MaxValue)]
        [AllowAnonymous]
        public ActionResult GetMainCategoryInterests()
        {
            var results = _interestTasks.GetMainCategoryInterests().ToList().Select(x => new { Text = x.Name, Value = x.Id }).ToList();
            return new JsonNetResult(results);
        }
    }
}