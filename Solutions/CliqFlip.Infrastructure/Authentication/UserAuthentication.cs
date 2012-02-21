﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CliqFlip.Infrastructure.Authentication.Interfaces;

namespace CliqFlip.Infrastructure.Authentication
{
    public class UserAuthentication : IUserAuthentication
    {
        public void Login(string username, bool stayLoggedIn)
        {
            //TODO: Set HTTPOnly  = true;
            //TODO: Set Secure = true when we use ssl;
            FormsAuthentication.SetAuthCookie(username, stayLoggedIn);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}