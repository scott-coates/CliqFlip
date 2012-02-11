using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CliqFlip.Web.Mvc.Extensions.Authentication
{
    public class UserAuthentication : IUserAuthentication
    {
        public void Login(string username, bool stayLoggedIn)
        {
            FormsAuthentication.SetAuthCookie(username, stayLoggedIn);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}