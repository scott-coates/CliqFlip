using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CliqFlip.Infrastructure.Images.Interfaces
{
	public interface IImageUploadService
	{
		ImageUploadResult UploadImage(HttpPostedFileBase profileImage);
	}
}
