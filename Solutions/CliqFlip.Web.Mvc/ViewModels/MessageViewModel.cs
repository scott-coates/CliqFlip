using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Web.Mvc.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        public string Sender { get; set; }
        public string SenderImageUrl { get; set; }
    }
}
