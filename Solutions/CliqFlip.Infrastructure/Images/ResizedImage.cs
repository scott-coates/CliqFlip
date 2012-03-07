using System.IO;

namespace CliqFlip.Infrastructure.Images
{
	public class ResizedImage
	{
		//TODO: Not sure if we should send back byte[] instead of stream - could be risky
		public Stream Image { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}