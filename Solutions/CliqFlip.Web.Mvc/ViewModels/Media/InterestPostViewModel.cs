
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.UserInterest;

namespace CliqFlip.Web.Mvc.ViewModels.Media
{
	public abstract class InterestPostViewModel
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

		protected InterestPostViewModel(PostDto post)
		{
			PostId = post.PostId;
			MediumType = post.MediumType;
			Description = post.Description;
			ThumbImage = post.ThumbImage;
			MediumImage = post.MediumImage;
			FullImage = post.FullImage;
			VideoUrl = post.VideoUrl;
			WebSiteUrl = post.WebSiteUrl;
			Title = post.Title;
		}
	}
}