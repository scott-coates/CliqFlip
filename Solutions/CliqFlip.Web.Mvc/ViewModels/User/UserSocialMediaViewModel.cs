using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
    public class UserSocialMediaViewModel : UserProfileViewModel
    {
        public string TwitterUsername { get; set; }
        public string YouTubeUsername { get; set; }
		public string WebsiteFeedUrl { get; set; }
		public string FacebookUsername { get; set; } //TODO: rename to facebook access code
    }
}
