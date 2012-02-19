using System.Collections.Generic;
using System.IO;
using CliqFlip.Infrastructure.IO.Interfaces;

namespace CliqFlip.Infrastructure.IO
{
	public class S3FileUploadService : IFileUploadService
	{
		public string[] UploadFiles(params Stream[] files)
		{
			return null;
		}
	}
}