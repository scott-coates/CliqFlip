using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliqFlip.Domain.Dtos;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserProfileIndexViewModel : UserProfileViewModel
	{
		public string Bio { get; set; }
        public string TwitterUsername { get; set; }
        public string YouTubeUsername { get; set; }
        public string WebsiteUrl { get; set; }
		public string InterestsJson { get; set; }
		public string SaveHeadlineUrl { get; set; }
		public string SaveBioUrl { get; set; }
		public string SaveMindMapUrl { get; set; }
        public string SaveYouTubeUsernameUrl { get; set; }
        public string SaveWebsiteUrl { get; set; }
        public string SaveTwitterUsernameUrl { get; set; }
		public bool CanEdit { get; set; }
        public string FacebookUsername { get; set; } // TODO: make this a bool that describes if the account is active
    }
}