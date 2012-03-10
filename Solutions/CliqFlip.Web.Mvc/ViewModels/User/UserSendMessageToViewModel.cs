using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserSendMessageToViewModel
    {
        [Required]
        public String Username { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public String Text { get; set; }
    }
}
