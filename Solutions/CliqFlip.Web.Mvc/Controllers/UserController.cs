using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserTasks _userTasks;

        public UserController(IUserTasks profileTasks)
        {
            this._userTasks = profileTasks;
        }

        public ViewResult Create()
        {
            return View(new UserCreate());
        }

        [HttpPost]
		[Transaction]
        public ActionResult Create(UserCreate profile)
        {
            if (ModelState.IsValid)
            {
                UserDto profileToCreate = new UserDto { Email = profile.Email, Password = profile.Password, Username = profile.Username };

                foreach (var interest in profile.UserInterests)
                {
                    var userInterest = new InterestDto(0, interest.Name, interest.Category, interest.Sociality);
                    profileToCreate.InterestDtos.Add(userInterest);
                }

                UserDto newProfile = _userTasks.Create(profileToCreate);

                //There was a problem creating the account
                //Username/Email already exists
                if (newProfile == null)
                {
                    return View(profile);
                }

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

		[Transaction]
		public ActionResult Index()
		{
			return View();
		}
    }
}