using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.ViewModels.Home;
using CliqFlip.Web.Mvc.ViewModels.Search;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserTasks _userTasks;
        private readonly IPrincipal _principal;
        private static readonly Dictionary<string, string> _invites =
            new Dictionary<string, string>
			{
				{"oc-invite","Welcome Orange County users!" },
				{"facebook-invite","Welcome Facebook friends!" },
				{"early-bird-invite-le1","Welcome early birds! Thanks for registering!" }, //launch effect 1,2,3,4,5
				{"early-bird-invite-le2","Welcome early birds! Thanks for registering!" },
				{"early-bird-invite-le3","Welcome early birds! Thanks for registering!" },
				{"early-bird-invite-le4","Welcome early birds! Thanks for registering!" },
				{"early-bird-invite-le5","Welcome early birds! Thanks for registering!" },
				{"beta-invite","Welcome beta users!" }
			};

        public HomeController(IPrincipal principal, IUserTasks userTasks)
        {
            _principal = principal;
            _userTasks = userTasks;
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

        public ActionResult Bootstrap()
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);

            var viewModel = new HomeUserViewModel
            {
                Username = _principal.Identity.Name,
                ProfileImageUrl = user.ProfileImage != null ? user.ProfileImage.ImageData.MediumFileName : "/Content/assets/images/empty-avatar.jpg",
                FriendCount = 0,
                PostCount = user.Posts.Count(),
                Interests = user.Interests.Select(x => new HomeUserViewModel.InterestViewModel { Name = x.Interest.Name, Id = x.Id })
                .OrderBy(x => x.Name)
                .ToList()
            };

            viewModel.InterestCount = viewModel.Interests.Count;

            return View(viewModel);
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