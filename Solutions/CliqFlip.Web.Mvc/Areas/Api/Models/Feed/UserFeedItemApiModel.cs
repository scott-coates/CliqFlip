namespace CliqFlip.Web.Mvc.Areas.Api.Models.Feed
{
    public class UserFeedItemApiModel
    {
        public string FeedItemType
        {
            get { return "User"; }
        }

        public string Username { get; set; }
        public string UserPageUrl { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}