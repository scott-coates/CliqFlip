﻿using System.Security.Principal;
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
					//TODO: Use a service for setting the cookie - unit tests will fail
					FormsAuthentication.SetAuthCookie(model.Username, model.LogMeIn);
					return Content("Awesome! You are now logged in.");
				}
			}
			//TODO: Return a partial view instead of inline html
			return Content("<strong>Login failed!</strong><br/> Please verify your username and password.");
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return Redirect("~");
		}

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
			var retVal = new JeipSaveResponseViewModel {html = saveTextViewModel.New_Value, is_error = false};
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
			var retVal = new JeipSaveResponseViewModel {html = saveTextViewModel.New_Value, is_error = false};
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