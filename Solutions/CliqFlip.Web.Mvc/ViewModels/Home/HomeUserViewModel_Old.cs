using System;
using System.Collections.Generic;
using CliqFlip.Common;

namespace CliqFlip.Web.Mvc.ViewModels.Home
{
    public class HomeUserViewModel_Old
    {
        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }
        public int PostCount { get; set; }
        public int InterestCount { get; set; }
        public int FriendCount { get; set; }
        public IList<InterestViewModel> Interests { get; set; }

        public class InterestViewModel
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}