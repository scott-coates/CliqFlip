using System.IO;

namespace CliqFlip.Domain.Dtos.Media
{
	public class FileStreamDto
	{
		public Stream Stream { get; set; }
		public string FileName { get; set; }

		public FileStreamDto(Stream stream, string fileName)
		{
			Stream = stream;
			FileName = fileName;
		}
	}
}