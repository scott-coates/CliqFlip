using System.Collections.Generic;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Interfaces;

namespace CliqFlip.Web.Mvc.ViewModels.User
{
	public class UserInterestsViewModel : UserProfileViewModel
	{
		public IList<InterestViewModel> Interests { get; set; }
		public string MakeDefaultUrl { get; set; }
		public string RemoveImageUrl { get; set; }

		public UserInterestsViewModel()
		{
			Interests = new List<InterestViewModel>();
		}

		#region Nested type: TagCloudInterestsViewModel

		public class InterestViewModel
		{
			public string Name { get; set; }
			public int InterestId { get; set; }
			public int UserInterestId { get; set; }
			public bool VisitorSharesThisInterest { get; set; }
            public IList<InterestMediumViewModel> Images { get; set; }

			public InterestViewModel()
			{
                Images = new List<InterestMediumViewModel>();
			}
		}

        public class InterestMediumViewModel
        {
            public InterestMediumViewModel(Medium medium)
            {
                MediumId = medium.Id;
                MediumType = medium.GetType().Name;
                Description = medium.Description;

                //set the preview images for media type that have images. Some medium's(website) wont have images all the time
                var mediaWithImages = medium as IHasImage;
                if (mediaWithImages != null && mediaWithImages.ImageData != null)
                {
                    ThumbImage = mediaWithImages.ImageData.ThumbFileName;
                    MediumImage = mediaWithImages.ImageData.MediumFileName;
                    FullImage = mediaWithImages.ImageData.FullFileName;
                }

                switch (MediumType)
                {
                    case "Video":
                        var video = (Video)medium;
                        VideoUrl = video.VideoUrl;
                        Title = video.Title;
                        break;
                    case "WebPage":
                        var website = (WebPage)medium;
                        Title = website.Title;
                        WebSiteUrl = website.LinkUrl;
                        MediumType += MediumImage == null ? "NoImage" : "";
                        break;
                }
            }
            public int MediumId { get; set; }
            public string MediumType { get; set; }
            public string Description { get; set; }
            public string ThumbImage { get; set; }
            public string MediumImage { get; set; }
            public string FullImage { get; set; }
            public string VideoUrl { get; set; }
            public string WebSiteUrl { get; set; }
            public string Title { get; set; }
        }

		#endregion
	}
}