using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Interfaces;

namespace CliqFlip.Domain.Dtos.Media
{
	public class PostDto
	{
		public int PostId { get; set; }
		public string MediumType { get; set; }
		public string Description { get; set; }
		public string ThumbImage { get; set; }
		public string MediumImage { get; set; }
		public string FullImage { get; set; }
		public string VideoUrl { get; set; }
		public string WebSiteUrl { get; set; }
		public string Title { get; set; }

		public PostDto(Post post)
		{
            Description = post.Description;
            PostId = post.Id;

		    var medium = post.Medium;
		    if (medium != null)
		    {
		        MediumType = medium.GetType().Name;

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
		}
	}
}