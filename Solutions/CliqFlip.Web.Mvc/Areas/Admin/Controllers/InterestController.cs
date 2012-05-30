using System.Web.Mvc;
using CliqFlip.Web.Mvc.Areas.Admin.Queries.Interfaces;
using CliqFlip.Web.Mvc.Security.Attributes;
using SharpArch.NHibernate.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[FormsAuthReadUserData(Order = 0)]
	[Authorize(Roles = "Administrator,Management", Order = 1)]
	public class InterestController : Controller
	{
		private readonly IInterestListQuery _interestListQuery;

		public InterestController(IInterestListQuery interestListQuery)
		{
			_interestListQuery = interestListQuery;
		}

		//
		// GET: /Notifications/

		[Transaction]
		public ViewResult Index(int? page)
		{
			return View(_interestListQuery.GetInterestList(page));
		}
	}
}