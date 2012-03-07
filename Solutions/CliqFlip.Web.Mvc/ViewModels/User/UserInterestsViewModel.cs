﻿using System.Collections.Generic;
using CliqFlip.Domain.Entities;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserInterestsViewModel : UserProfileViewModel
	{
		public IList<InterestViewModel> Interests { get; set; }
		public bool CanEdit { get; set; }

		public UserInterestsViewModel()
		{
			Interests = new List<InterestViewModel>();
		}

		#region Nested type: TagCloudInterestsViewModel

		public class InterestViewModel
		{
			public string Name { get; set; }
			public int UserInterestId { get; set; }
			public InterestImageViewModel DefaultImage { get; set; }
			public IList<InterestImageViewModel> Images { get; set; }

			public InterestViewModel()
			{
				Images = new List<InterestImageViewModel>();
			}
		}

		public class InterestImageViewModel
		{
			public InterestImageViewModel(Image image)
			{
				ThumbImage = image.Data.ThumbFileName;
				MediumImage = image.Data.MediumFileName;
				FullImage = image.Data.FullFileName;
			}

			public string ThumbImage { get; set; }
			public string MediumImage { get; set; }
			public string FullImage { get; set; }
		}
		#endregion
	}
}