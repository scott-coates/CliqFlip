using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Domain;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Web.Mvc.ViewModels.User;

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
        public ActionResult Create(UserCreate profile)
        {
            if (ModelState.IsValid)
            {
                UserDto profileToCreate = new UserDto { Email = profile.Email, Password = profile.Password, Username = profile.Username };

                foreach (var interest in profile.Interests)
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

                return RedirectToAction("Details", "Profile", new { id = newProfile.Username });
            }
            return View(profile);
        }
    }
}