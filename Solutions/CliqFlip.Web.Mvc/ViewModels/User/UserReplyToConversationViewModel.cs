using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserReplyToConversationViewModel
    {
        //conversation id
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}