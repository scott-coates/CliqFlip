using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Home;
using CliqFlip.Web.Mvc.ViewModels.Search;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
	using System.Web.Mvc;

	public class SearchController : Controller
	{
		private readonly ISubjectTasks _subjectTasks;
		private readonly IUsersByInterestsQuery _usersByInterestsQuery;

		public SearchController(ISubjectTasks subjectTasks, IUsersByInterestsQuery usersByInterestsQuery)
		{
			_subjectTasks = subjectTasks;
			_usersByInterestsQuery = usersByInterestsQuery;
		}

		[Transaction]
		public ActionResult Index(string as_values_search_values)
		{
			//TODO: Look into using something like this for posted objects - http://stackoverflow.com/questions/4316301/asp-net-mvc-2-bind-a-models-property-to-a-different-named-value

			var viewModel = _usersByInterestsQuery.GetGetUsersByInterests(as_values_search_values);

			return View(viewModel);
		}
	}
}