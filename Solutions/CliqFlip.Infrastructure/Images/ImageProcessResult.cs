using System.IO;

namespace CliqFlip.Infrastructure.Images
{
	public class ImageProcessResult
	{
		//TODO: we might want to store the original too someday

		public ResizedImage ThumbnailImage { get; set; }
		public ResizedImage MediumImage { get; set; }
		public ResizedImage FullImage { get; set; }
	}
}