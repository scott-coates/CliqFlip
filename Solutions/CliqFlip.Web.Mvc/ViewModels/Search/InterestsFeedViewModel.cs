using System.Collections.Generic;

using CliqFlip.Domain.Dtos.Interest;
using CliqFlip.Domain.Entities;
using CliqFlip.Web.Mvc.ViewModels.Media;
using CliqFlip.Web.Mvc.ViewModels.User;
using Newtonsoft.Json;

namespace CliqFlip.Web.Mvc.ViewModels.Search
{
	public class InterestsFeedViewModel
	{
		[JsonProperty("total")]
		public int Total { get; set; }

		[JsonProperty("data")]
		public IList<FeedMediumViewModel> InterestViewModels { get; set; }

		public InterestsFeedViewModel()
		{
			InterestViewModels = new List<FeedMediumViewModel>();
		}

		public class FeedMediumViewModel : InterestMediumViewModel
		{
			public string Username { get; set; }
			public string UserPageUrl { get; set; }
			public string ImageUrl { get; set; }
			public string Interest { get; set; }

			public FeedMediumViewModel(InterestFeedItemDto feedItemDto) : base(feedItemDto.Medium)
			{
				Username = feedItemDto.Username;
				ImageUrl = feedItemDto.ImageUrl;
				Interest = feedItemDto.Interest;
			}
		}
	}
}