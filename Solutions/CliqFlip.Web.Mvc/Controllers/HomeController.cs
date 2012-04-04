using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Common;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPrincipal _principal;
		private static readonly Dictionary<string, string> _invites =
			new Dictionary<string, string>
			{
				{"oc-invite","Welcome Orange County users!" },
				{"beta-invite","Welcome beta users!" }
			};

		public HomeController(IPrincipal principal)
		{
			_principal = principal;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			if (_principal.Identity.IsAuthenticated)
			{
				return RedirectToRoute(Constants.ROUTE_LANDING_PAGE);
			}

			return View();
		}

		[AllowAnonymous]
		public ActionResult Invite(string inviteKey)
		{
			if (_principal.Identity.IsAuthenticated)
			{
				return RedirectToRoute(Constants.ROUTE_LANDING_PAGE);
			}

			inviteKey = inviteKey.ToLowerInvariant();

			if (!_invites.ContainsKey(inviteKey))
			{
				throw new HttpException((int)HttpStatusCode.Forbidden, "You do not have access to this invite");
			}

			ViewBag.InviteKey = inviteKey;
			ViewBag.InviteMessage = _invites[inviteKey];

			return View();
		}
	}
}