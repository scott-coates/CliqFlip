using System.IO;

namespace CliqFlip.Infrastructure.IO
{
	public class FileToUpload
	{
		public Stream Stream { get; private set; }
		public string Filename { get; private set; }

		public FileToUpload(Stream stream, string filename)
		{
			Filename = filename;
			Stream = stream;
		}
	}
}