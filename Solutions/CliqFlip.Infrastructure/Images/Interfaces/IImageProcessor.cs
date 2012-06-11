using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using CliqFlip.Domain.Dtos.Media;

namespace CliqFlip.Infrastructure.Images.Interfaces
{
	public interface IImageProcessor
	{
		ImageProcessResult ProcessImage(FileStreamDto profileImage);
	}
}
