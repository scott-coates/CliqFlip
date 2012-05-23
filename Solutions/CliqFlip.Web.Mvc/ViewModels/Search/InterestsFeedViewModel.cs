﻿using System.Collections.Generic;
using CliqFlip.Domain.Dtos;
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
			public FeedMediumViewModel(MediumDto medium) : base(medium)
			{
			}
		}
	}
}