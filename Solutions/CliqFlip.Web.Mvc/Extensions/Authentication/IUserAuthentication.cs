using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Web.Mvc.Extensions.Authentication
{
    public interface IUserAuthentication
    {
        void Login(string username, bool stayLoggedIn);
        void Logout();
    }
}