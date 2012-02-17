using System.IO;

namespace CliqFlip.Infrastructure.Images
{
	public class ImageProcessResult
	{
		//TODO: Not sure if we should send back byte[] instead of stream - could be risky
		public Stream ThumbnailImage { get; set; }
		public Stream MediumImage { get; set; }
		public Stream FullImage { get; set; }
		//we might want to store the original too someday
	}
}