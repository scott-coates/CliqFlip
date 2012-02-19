using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CliqFlip.Infrastructure.IO.Interfaces
{
	public interface IFileUploadService
	{
		string[] UploadFiles(params Stream[] files);
	}
}
