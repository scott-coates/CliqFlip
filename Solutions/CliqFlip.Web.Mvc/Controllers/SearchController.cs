using System;
using System.Collections.Generic;
using System.Linq;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Home;
using CliqFlip.Web.Mvc.ViewModels.Search;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;
using MvcContrib.Pagination;

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
		public ActionResult Index(string q, int? page)
		{
			var viewModel = _usersByInterestsQuery.GetGetUsersByInterests(q, page);

			return View(viewModel);
		}
	}
}