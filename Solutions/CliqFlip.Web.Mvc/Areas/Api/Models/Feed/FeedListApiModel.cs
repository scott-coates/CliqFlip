using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Dtos.Post;
using Newtonsoft.Json;

namespace CliqFlip.Web.Mvc.Areas.Api.Models.Feed
{
    public class FeedListApiModel
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("returned")]
        public int TotalReturned { get; set; }
        [JsonProperty("data")]
        public List<dynamic> FeedItems { get; set; }

        public FeedListApiModel()
        {
            FeedItems = new List<dynamic>();
        }
    }
}