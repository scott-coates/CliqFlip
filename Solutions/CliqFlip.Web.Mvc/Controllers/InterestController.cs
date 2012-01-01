using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Contracts.Tasks;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class InterestController : Controller
    {
        private Domain.Contracts.Tasks.IInterestTasks _interestTasks;

        public InterestController(IInterestTasks subjectTasks)
        {
            this._interestTasks = subjectTasks;
        }

        public JsonResult Search(string keyword)
        {
            var results = _interestTasks.GetInterestDtos().Where(c => c.Name.ToLower().Contains(keyword.ToLower())).ToList();
            return Json(results , JsonRequestBehavior.AllowGet);
        }
    }
}