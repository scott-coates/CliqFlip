using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.Extensions.Validation;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserCreate
    {
        public UserCreate()
        {
            Interests = new List<InterestCreate>();
        }

        [Required(ErrorMessage = "Please choose a username")]
        [Display(Name = "Choose a username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide an email address.")]
        [Display(Name = "Whats your email?")]
        [Email(ErrorMessage="Please provide a valid email address.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please provide a password.")]
        [Display(Name = "Set your password. Choose a strong one.")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Please type your password again.")]
        [Compare("Password", ErrorMessage="Password do not match")]
        [Display(Name = "Please type your password again")]
        [DataType(DataType.Password)]
        public String PasswordVerify { get; set; }

        public List<InterestCreate> Interests { get; set; }

    }

    public class InterestCreate
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Category { get; set; }
        public int Sociality { get; set; }
    }
}