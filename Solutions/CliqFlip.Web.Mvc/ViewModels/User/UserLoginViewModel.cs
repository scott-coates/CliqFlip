using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required]
        public String Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name="Log me in")]
        public bool LogMeIn { get; set; }
    }
}