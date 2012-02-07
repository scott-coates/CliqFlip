using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Jeip;
using CliqFlip.Web.Mvc.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserProfileQuery _userProfileQuery;
		private readonly IUserTasks _userTasks;

		public UserController(IUserTasks profileTasks, IUserProfileQuery userProfileQuery)
		{
			_userTasks = profileTasks;
			_userProfileQuery = userProfileQuery;
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
				var profileToCreate = new UserDto {Email = profile.Email, Password = profile.Password, Username = profile.Username};

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

				//TODO: delegate this responsibiliy to userTask - unit test will fail this static method
				//TODO: Set HTTPOnly  = true;
				//TODO: Set Secure = true when we use ssl;
				FormsAuthentication.SetAuthCookie(newProfile.Username, false);
				return RedirectToAction("Details", "User", new {id = "me"});
			}
			return View(profile);
		}


		//[HttpPost]
		public ActionResult Login([Required] string username, [Required] string password, bool stayLoggedIn)
		{
			if (ModelState.IsValid)
			{
				if (_userTasks.ValidateUser(username, password))
				{
					FormsAuthentication.SetAuthCookie(username, stayLoggedIn);
					//TODO:  Redirect to users profile
					return RedirectToAction("Details", "User", new {id = "me"});
				}
			}
			ViewData["username"] = username;
			ViewData["password"] = password;
			ViewData["stayLoggedIn"] = stayLoggedIn;
			return View();
		}


		[HttpPost]
		[Transaction]
		public ActionResult SaveMindMap(UserSaveMindMapViewModel userSaveMindMapViewModel)
		{
			_userTasks.UpdateMindMap(userSaveMindMapViewModel.UserId,
			                         userSaveMindMapViewModel.Interests
			                         	.Select(x =>
			                         	        new UserInterestDto(x.Id, null, null, null, null, x.Passion, x.XAxis, x.YAxis))
			                         	.ToList());
			return new EmptyResult();
		}


		[HttpPost]
		[Transaction]
		public ActionResult SaveHeadline(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
			var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false};
			return new JsonNetResult(retVal);

		}

		[HttpPost]
		[Transaction]
		public ActionResult SaveBio(JeipSaveTextViewModel saveTextViewModel)
		{
			//get user and save it
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
			return Index(username);
		}
	}
}