using System;
using System.Linq;
using CliqFlip.Domain.Contracts.Queries;
using CliqFlip.Domain.Contracts.Tasks;

namespace CliqFlip.Web.Mvc.Controllers
{
	using System.Web.Mvc;

	public class SearchController : Controller
	{
		private readonly IInterestTasks _interestTasks;
		private readonly IUsersByInterestsQuery _usersByInterestsQuery;

		public SearchController(IInterestTasks interestTasks, IUsersByInterestsQuery usersByInterestsQuery)
		{
			_interestTasks = interestTasks;
			_usersByInterestsQuery = usersByInterestsQuery;
		}

		[HttpPost]
		public ActionResult Index(string as_values_post)
		{
			//TODO: Look into using something like this for posted objects - http://stackoverflow.com/questions/4316301/asp-net-mvc-2-bind-a-models-property-to-a-different-named-value

			var ids = as_values_post.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

			int parseint = 0;
			
			var vals = (from id in ids where int.TryParse(id, out parseint) select parseint).ToList();

			//var viewModel = _usersByInterestsQuery.GetGetUsersByInterests(vals);
			return null;
			//return View(viewModel);
		}
	}
}