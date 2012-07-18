using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class FeedPostOverviewViewModel
	{
        public int PostId { get; set; }
        public string Username { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string WebPageUrl { get; set; }
        public string AuthorImageUrl { get; set; }
        public string ImageDescription { get; set; }
        public bool HasCommonIntersts { get; set; }
        public bool IsLikedByUser { get; set; } 

        public IList<ActivityViewModel> Activity { get; set; }
        public IList<CommonInterestViewModel> CommonInterests { get; set; }

        public class ActivityViewModel
        {
            public string Username { get; set; }
            public string ProfileImageUrl { get; set; }
            public string CommentText { get; set; }
        }

        public class CommonInterestViewModel
        {
            public string Name { get; set; }
        }

    }
}