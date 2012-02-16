namespace CliqFlip.Infrastructure.Images
{
	public class ImageProcessResult
	{
		public byte[] ThumbnailImage { get; set; }
		public byte[] MediumImage { get; set; }
		public byte[] FullImage { get; set; }
		//we might want to store the original too someday
	}
}