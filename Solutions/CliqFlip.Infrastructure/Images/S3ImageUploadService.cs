using System;
using System.Web;
using System.Web.Helpers;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Images.Interfaces;

namespace CliqFlip.Infrastructure.Images
{
	public class S3ImageUploadService : IImageUploadService
	{
		private const int MIN_RESOLUTION = 180;
		private const int MAX_RESOLUTION = 2048;
		private const string MIN_RESOLUTION_MESSAGE = "The minimum resolution is 180 pixels. Please upload a larger file";
		private const string MAX_RESOLUTION_MESSAGE = "The maximum resolution is 2048 pixels. Please upload a smaller file";

		#region IImageUploadService Members

		public ImageUploadResult UploadImage(HttpPostedFileBase profileImage)
		{
			ImageUploadResult retVal = null;

			WebImage image = WebImage.GetImageFromRequest(profileImage.FileName);

			ValidateImageSize(image);

			return retVal;
		}

		#endregion

		private static void ValidateImageSize(WebImage image)
		{
			if (image == null) throw new ArgumentNullException("image");

			if (image.Width < MIN_RESOLUTION || image.Height < MIN_RESOLUTION)
			{
				throw new RulesException("image", MIN_RESOLUTION_MESSAGE);
			}
			if (image.Width > MAX_RESOLUTION || image.Height > MAX_RESOLUTION)
			{
				throw new RulesException("image", MAX_RESOLUTION_MESSAGE);
			}
		}
	}
}