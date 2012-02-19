using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.IO.Interfaces
{
	public interface IFileUploadService
	{
		IList<string> UploadFiles(string path, IList<FileToUpload> files);
		void DeleteFiles(string path, IList<string> fileNames);
	}
}
