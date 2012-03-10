using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Web.Mvc.ViewModels.User;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserInboxViewModel : UserProfileViewModel
    {
        public IList<ConversationViewModel> Conversations { get; set; }

        public class ConversationViewModel
        {
            public long Id { get; set; }
            public bool HasUnreadMessages { get; set; }
            public string SenderImage { get; set; }
            public string Sender { get; set; }
            public string LastMessage { get; set; }
        }

        public UserInboxViewModel()
        {
            Conversations = new List<ConversationViewModel>();
        }
    }
}
