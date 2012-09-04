using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliqFlip.Domain.Common
{
    public static class Constants
    {
        public const string RETURN_URL = "ReturnUrl";
        public const string GA_ID = "GoogleAnalyticsId";
        public const string S3_BUCKET = "S3Bucket";
        public const string SES_FROM_EMAIL = "SESFromEmail";
        public const string FACEBOOK_APPID = "FacebookAppId";
        public const string DEFAULT_PROFILE_IMAGE = "/Content/assets/images/empty-avatar.jpg";
        public const string YAHOO_APP_ID = "YahooAppId";
        public const string LOCATION_SESSION_KEY = "LocationSessionKey";
        public const string ROUTE_USER_HOME_PAGE = "User Home Page";
        public const string NOTIFICATION_COOKIE = "notification-id";
        public const int FEED_LIMIT = 10;
        public const string SUCCESS_TEMPDATA_KEY = "success";
        public const string ERROR_TEMPDATA_KEY = "error";
        public const string GRAPH_URL = "graph";
        public const string RABBIT_MQ_URI = "queue";

        /*User Search Pipeline*/
        public const string INTEREST_MAX_HOPS = "3";
        public const float MAIN_CATEGORY_INTEREST_MULTIPLIER = .5f;
        public static readonly float[] EXPLICIT_SEARCH_INTEREST_MULTIPLIER = new[]{ 5f, 3f };

        //anything over INTEREST_MAX_HOPS + 1 will not allow an interest unless it was explicitly searched for
        public const float EXPLICIT_SEARCH_INTEREST_THRESHOLD = 4;
        public const float CLOSE_INTEREST_THRESHOLD = 2.25f;
        public const float LOCATION_MILE_MULTIPLIER = .01f;
        public const int LOCATION_MAX_MILES = 100;
    }
}