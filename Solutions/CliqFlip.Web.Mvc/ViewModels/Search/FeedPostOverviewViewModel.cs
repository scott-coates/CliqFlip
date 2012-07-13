using System.Collections.Generic;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class FeedPostOverviewViewModel
	{
        public string Username { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorImageUrl { get; set; }
        public string ImageDescription { get; set; }

        public IList<ActivityViewModel> Activity { get; set; }

        public class ActivityViewModel
        {
            public string Username { get; set; }
            public string ProfileImageUrl { get; set; }
            public string CommentText { get; set; }
        }
	}
}