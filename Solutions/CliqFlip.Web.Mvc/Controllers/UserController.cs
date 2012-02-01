using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserTasks _userTasks;
    	private readonly IUserProfileQuery _userProfileQuery;
        public UserController(IUserTasks profileTasks, IUserProfileQuery userProfileQuery)
        {
        	this._userTasks = profileTasks;
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
                UserDto profileToCreate = new UserDto { Email = profile.Email, Password = profile.Password, Username = profile.Username };

                foreach (var interest in profile.UserInterests)
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
                return RedirectToAction("Details", "User", new { id = "me" });
            }
            return View(profile);
        }


        //[HttpPost]
        public ActionResult Login([Required]string username, [Required]string password, bool stayLoggedIn)
        {
            if (ModelState.IsValid)
            {
                if (_userTasks.ValidateUser(username, password))
                {
                    FormsAuthentication.SetAuthCookie(username, stayLoggedIn);
                    //TODO:  Redirect to users profile
                    return RedirectToAction("Details", "User", new { id = "me" });
                }
            }
            ViewData["username"] = username;
            ViewData["password"] = password;
            ViewData["stayLoggedIn"] = stayLoggedIn;
            return View();
        }


		[HttpPost]
		[Transaction]
		public ActionResult SaveMindMap(List<UserInterestDto> userInterests )
		{
			return new EmptyResult();
		}

		[Transaction]
		public ActionResult Index(string username)
		{
			var user = _userProfileQuery.GetUser(username);
			user.SaveMindMapUrl = "\"" + Url.Action("SaveMindMap", "User") + "\"";
			return View(user);
		}
    }
}