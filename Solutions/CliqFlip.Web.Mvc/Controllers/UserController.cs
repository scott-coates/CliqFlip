using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Web.Mvc.Extensions.Exceptions;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Jeip;
using CliqFlip.Web.Mvc.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class UserController : Controller
	{
		private readonly IPrincipal _principal;
		private readonly IUserProfileQuery _userProfileQuery;
		private readonly IUserTasks _userTasks;

		public UserController(IUserTasks profileTasks, IUserProfileQuery userProfileQuery, IPrincipal principal)
		{
			_userTasks = profileTasks;
			_userProfileQuery = userProfileQuery;
			_principal = principal;
		}

		public ActionResult Create()
		{
			if (_principal.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "User", new { username = _principal.Identity.Name });
			}

			return View(new UserCreateViewModel());
		}

		[HttpPost]
		[Transaction]
		public ActionResult Create(UserCreateViewModel profile)
		{
			if (ModelState.IsValid)
			{
				var profileToCreate = new UserDto { Email = profile.Email, Password = profile.Password, Username = profile.Username };

				foreach (InterestCreate interest in profile.UserInterests)
				{
					var userInterest = new UserInterestDto(0, interest.Name, interest.Category, interest.Sociality);
					profileToCreate.InterestDtos.Add(userInterest);
				}

				User user = _userTasks.Create(profileToCreate);

				//There was a problem creating the account
				//Username/Email already exists
				if (user == null)
				{
					//TODO: don't return null - throw exception 
					return View(profile);
				}

				_userTasks.Login(user, false);

				return RedirectToAction("Index", "User", new { username = profile.Username });
			}

			//TODO: Implement PRG pattern for post forms
			return View(profile);
		}

		public ActionResult Login()
		{
			return PartialView();
		}

		[HttpPost]
		public ActionResult Login(UserLoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (_userTasks.Login(model.Username, model.Password, model.LogMeIn))
				{
					return Content("Awesome! You are now logged in.");
				}
			}
			//TODO: Return a partial view instead of inline html
			return Content("<strong>Login failed!</strong><br/> Please verify your username and password.");
		}

		public ActionResult Logout()
		{
			_userTasks.Logout(_principal.Identity.Name);
			return Redirect("~");
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveProfileImage(HttpPostedFileBase profileImage)
		{
			//http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx
			//model bound
			User user = _userTasks.GetUser(_principal.Identity.Name);
			if (profileImage == null)
			{
				ViewData.ModelState.AddModelError("Image", "You need to provide a file first... or don't. Have it your way.");
				RouteData.Values["action"] = "Index";
				return Index(_principal.Identity.Name);
			}
			else
			{
				try
				{
					_userTasks.SaveProfileImage(user, profileImage);
				}
				catch (RulesException rex)
				{
					//TODO: Implement PRG pattern for post forms
					//TODO: Log These exceptions in elmah
					rex.AddModelStateErrors(ModelState);
					RouteData.Values["action"] = "Index";
					return Index(_principal.Identity.Name);
				}
				finally
				{
					profileImage.InputStream.Dispose();
				}
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveInterestImage(UserSaveInterestImageViewModel userSaveInterestImageViewModel)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);
			if (userSaveInterestImageViewModel.ProfileImage == null)
			{
				ViewData.ModelState.AddModelError("Image", "You need to provide a file first... or don't. Have it your way.");
				RouteData.Values["action"] = "Interests";
				return Interests(_principal.Identity.Name);
			}
			else
			{
				try
				{
					_userTasks.SaveInterestImage(user, userSaveInterestImageViewModel.ProfileImage, userSaveInterestImageViewModel.UserInterestId, userSaveInterestImageViewModel.ImageDescription);
				}
				catch (RulesException rex)
				{
					//TODO: Implement PRG pattern for post forms
					//TODO: Log These exceptions in elmah
					rex.AddModelStateErrors(ModelState);
					RouteData.Values["action"] = "Interests";
					return Interests(_principal.Identity.Name);
				}
				finally
				{
					userSaveInterestImageViewModel.ProfileImage.InputStream.Dispose();
				}
			}

			return RedirectToAction("Interests");
		}

		[Authorize]
		[Transaction]
		public ActionResult MakeInterestImageDefault(int imageId)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);

			user.MakeInterestImageDefault(imageId);

			return RedirectToAction("Interests");
		}

		[Authorize]
		[Transaction]
		public ActionResult RemoveImage(int imageId)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);

			_userTasks.RemoveImage(user, imageId);

			return RedirectToAction("Interests");
		}

		[Authorize]
		[Transaction]
		public ActionResult RemoveInterest(int interestId)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);

			_userTasks.RemoveInterest(user, interestId);

			return RedirectToAction("Interests");
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveMindMap(UserSaveMindMapViewModel userSaveMindMapViewModel)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateInterest(userSaveMindMapViewModel.Id, new UserInterestOption(userSaveMindMapViewModel.Passion, userSaveMindMapViewModel.XAxis, userSaveMindMapViewModel.YAxis));

			return new EmptyResult();
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveHeadline(JeipSaveTextViewModel saveTextViewModel)
		{
			//TODO: look into XSS vulnerability
			//We're taking in raw text and sending it back
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateHeadline(saveTextViewModel.New_Value);
			var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
			return new JsonNetResult(retVal);
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveBio(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateBio(saveTextViewModel.New_Value);
			var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
			return new JsonNetResult(retVal);
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveTwitterUsername(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateTwitterUsername(saveTextViewModel.New_Value);
			var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
			return new JsonNetResult(retVal);
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveYouTubeUsername(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateYouTubeUsername(saveTextViewModel.New_Value);
			var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
			return new JsonNetResult(retVal);
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveWebiste(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
			User user = _userTasks.GetUser(_principal.Identity.Name);
			var retVal = new JeipSaveResponseViewModel();

			try
			{
				_userTasks.SaveWebsite(user, saveTextViewModel.New_Value);
				retVal.is_error = false;
				retVal.html = user.UserWebsite.SiteUrl;
			}
			catch (RulesException rex)
			{
				retVal.is_error = true;
				retVal.error_text = rex.Errors.First().ErrorMessage;
			}

			return new JsonNetResult(retVal);
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveFacebook(string fbid)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateFacebookUsername(fbid);
			return new JsonNetResult();
		}



		[Transaction]
		public ActionResult Index(string username)
		{
			UserProfileIndexViewModel user = _userProfileQuery.GetUserProfileIndex(username, _principal);
			if (user != null)
			{
				user.SaveHeadlineUrl = "\"" + Url.Action("SaveHeadline", "User") + "\"";
				user.SaveMindMapUrl = "\"" + Url.Action("SaveMindMap", "User") + "\"";
				user.SaveBioUrl = "\"" + Url.Action("SaveBio", "User") + "\"";
				user.SaveTwitterUsernameUrl = "\"" + Url.Action("SaveTwitterUsername", "User") + "\"";
				user.SaveYouTubeUsernameUrl = "\"" + Url.Action("SaveYouTubeUsername", "User") + "\"";
				user.SaveWebsiteUrl = "\"" + Url.Action("SaveWebiste", "User") + "\"";
				return View(user);
			}

			//http://stackoverflow.com/a/4985562/173957
			throw new HttpException(404, "Not found");
		}

		[Transaction]
		public ActionResult SocialMedia(string username)
		{
			UserSocialMediaViewModel user = _userProfileQuery.GetUserSocialMedia(username, _principal);
			return View(user);
		}

		[Transaction]
		public ActionResult Interests(string username)
		{
			UserInterestsViewModel user = _userProfileQuery.GetUserIntersets(username, _principal);
			user.MakeDefaultUrl = "\"" + Url.Action("MakeInterestImageDefault", "User") + "\"";
			user.RemoveImageUrl = "\"" + Url.Action("RemoveImage", "User") + "\"";

			return View(user);
		}
	}
}