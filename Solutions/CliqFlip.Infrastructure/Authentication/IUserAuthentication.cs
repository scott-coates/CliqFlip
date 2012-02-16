using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.Authentication
{
    public interface IUserAuthentication
    {
        void Login(string username, bool stayLoggedIn);
        void Logout();
    }
}