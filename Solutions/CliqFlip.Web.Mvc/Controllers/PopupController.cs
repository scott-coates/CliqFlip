using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.ViewModels.Popup;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class PopupController : Controller
	{
		private readonly IPrincipal _principal;
		private readonly IUserTasks _userTasks;

		public PopupController(IUserTasks userTasks, IPrincipal principal)
		{
			_userTasks = userTasks;
			_principal = principal;
		}

		[Authorize]
		public ActionResult BookmarkMedium(string mediumUrl)
		{
			if (string.IsNullOrWhiteSpace(mediumUrl))
			{
				throw new HttpException((int)HttpStatusCode.NotFound, "Not found");
			}
			var viewModel = new BookmarkMediumViewModel { MediumUrl = mediumUrl };
			User user = _userTasks.GetUser(_principal.Identity.Name);
			viewModel
				.Interests
				.AddRange(user
							.Interests
							.OrderBy(x=>x.Interest.Name)
							.Select(x => new BookmarkMediumViewModel.InterestsViewModel
							{
								UserInterestId = x.Id,
								InterestName = x.Interest.Name
							}));

			return View(viewModel);
		}
	}
}