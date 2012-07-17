using System;
using System.Collections.Generic;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Common;

namespace CliqFlip.Web.Mvc.ViewModels.Home
{
    public class HomeUserViewModel
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