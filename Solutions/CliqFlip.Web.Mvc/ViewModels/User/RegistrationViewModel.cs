using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Common;
using CliqFlip.Domain.ReadModels;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class RegistrationViewModel
    {
        public string Username { get; set; }

        public RegistrationViewModel(string username)
        {
            Username = username;
        }
    }
}
