using System.Collections.Generic;

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
        public string MajorLocationName { get; set; }

        public IList<InterestInCommonApiModel> InterestsInCommon { get; set; }
        public int CommonInterestCount { get; set; }
        public int RelatedInterestCount { get; set; }

        public class InterestInCommonApiModel
        {
            public string Name { get; set; }
            public bool IsExactMatch { get; set; }
        }
    }
}