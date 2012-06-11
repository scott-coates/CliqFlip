using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Dtos.Media;

namespace CliqFlip.Web.Mvc.ViewModels.Media
{
	public abstract class InterestMediumViewModel
	{
		public int MediumId { get; set; }
		public string MediumType { get; set; }
		public string Description { get; set; }
		public string ThumbImage { get; set; }
		public string MediumImage { get; set; }
		public string FullImage { get; set; }
		public string VideoUrl { get; set; }
		public string WebSiteUrl { get; set; }
		public string Title { get; set; }

		protected InterestMediumViewModel(MediumDto medium)
		{
			MediumId = medium.MediumId;
			MediumType = medium.MediumType;
			Description = medium.Description;
			ThumbImage = medium.ThumbImage;
			MediumImage = medium.MediumImage;
			FullImage = medium.FullImage;
			VideoUrl = medium.VideoUrl;
			WebSiteUrl = medium.WebSiteUrl;
			Title = medium.Title;
		}
	}
}