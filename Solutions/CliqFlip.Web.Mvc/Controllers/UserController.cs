using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Jeip;
using CliqFlip.Web.Mvc.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;
using CliqFlip.Web.Mvc.Extensions.Authentication;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class UserController : Controller
	{
		private readonly IPrincipal _principal;
		private readonly IUserProfileQuery _userProfileQuery;
		private readonly IUserTasks _userTasks;
        private readonly IUserAuthentication _userAuth;

        public UserController(IUserTasks profileTasks, IUserProfileQuery userProfileQuery, IPrincipal principal, IUserAuthentication userAuth)
		{
			_userTasks = profileTasks;
			_userProfileQuery = userProfileQuery;
			_principal = principal;
            _userAuth = userAuth;
		}

		public ViewResult Create()
		{
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

				UserDto newProfile = _userTasks.Create(profileToCreate);

				//There was a problem creating the account
				//Username/Email already exists
				if (newProfile == null)
				{
					//TODO: don't return null - throw exception 
					return View(profile);
				}

                _userAuth.Login(newProfile.Username, false);
				return RedirectToAction("Index", "User", new { username = profile.Username });
			}
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
				if (_userTasks.ValidateUser(model.Username, model.Password))
				{
                    _userAuth.Login(model.Username, model.LogMeIn);
					return Content("Awesome! You are now logged in.");
				}
			}
			//TODO: Return a partial view instead of inline html
			return Content("<strong>Login failed!</strong><br/> Please verify your username and password.");
		}

		public ActionResult Logout()
		{
            _userAuth.Logout();
			return Redirect("~");
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveMindMap(UserSaveMindMapViewModel userSaveMindMapViewModel)
		{
			User user = _userTasks.GetUser(_principal.Identity.Name);
			user.UpdateInterest(
				new UserInterest(
					userSaveMindMapViewModel.Id,
					new UserInterestOption(userSaveMindMapViewModel.Passion, userSaveMindMapViewModel.XAxis, userSaveMindMapViewModel.YAxis), null, null, null));

			return new EmptyResult();
		}

		[Authorize]
		[HttpPost]
		[Transaction]
		public ActionResult SaveHeadline(JeipSaveTextViewModel saveTextViewModel)
		{
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

		[Transaction]
		public ActionResult Index(string username)
		{
			UserProfileViewModel user = _userProfileQuery.GetUser(username);
			user.SaveHeadlineUrl = "\"" + Url.Action("SaveHeadline", "User") + "\"";
			user.SaveMindMapUrl = "\"" + Url.Action("SaveMindMap", "User") + "\"";
			user.SaveBioUrl = "\"" + Url.Action("SaveBio", "User") + "\"";
            user.SaveTwitterUsernameUrl = "\"" + Url.Action("SaveTwitterUsername", "User") + "\"";
            user.SaveYouTubeUsernameUrl = "\"" + Url.Action("SaveYouTubeUsername", "User") + "\"";
			user.CanEdit = _principal.Identity.Name.ToLower() == username.ToLower();

			return View(user);
		}

		[Transaction]
		public ActionResult Interests(string username)
		{
			return Index(username);
		}

		[Transaction]
		public ActionResult SocialMedia(string username)
		{
            UserProfileViewModel user = _userProfileQuery.GetUser(username);
            //user.CanEdit = _principal.Identity.Name.ToLower() == username.ToLower();

            //return View(user);
            //var model = new UserSocialMediaViewModel();
            //model.TwitterUsername = "cruzmjorge";
            //model.YouTubeUsername = "ChrisArchieProds";
            return View(user);
		}
	}
}