using System;
using System.Web;
using System.Web.Helpers;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Images.Interfaces;

namespace CliqFlip.Infrastructure.Images
{
	public class ImageProcessor : IImageProcessor
	{
		private const int MIN_RESOLUTION = 180;
		private const int MAX_RESOLUTION = 2048;
		private const int THUMBNAIL_RESOLUTION = 50;
		private const int MEDIUM_RESOLUTION = 200;
		private const int FULL_RESOLUTION = 800;
		private const string MIN_RESOLUTION_MESSAGE = "The minimum resolution is 180 pixels. Please upload a larger file";
		private const string MAX_RESOLUTION_MESSAGE = "The maximum resolution is 2048 pixels. Please upload a smaller file";

		#region IImageProcessor Members

		public ImageProcessResult ProcessImage(HttpPostedFileBase profileImage)
		{
			var retVal = new ImageProcessResult();

			WebImage image = WebImage.GetImageFromRequest(profileImage.FileName);

			ValidateImageSize(image);

			//We know the image input will always be bigger than thumbnail
			WebImage thumbnailImage = image.Resize(50, 50);
			retVal.ThumbnailImage = thumbnailImage.GetBytes();

			int mediumWidth = image.Width > MEDIUM_RESOLUTION ? MEDIUM_RESOLUTION : image.Width;

			WebImage mediumImage = image.Resize(MEDIUM_RESOLUTION, GetHeightAspectRatio(mediumWidth, image));
			retVal.MediumImage = mediumImage.GetBytes();

			if (mediumWidth > (MEDIUM_RESOLUTION + 50))
			{
				//the + 50 means don't create a full size image if it's barely bigger than a medium sized one

				//the image res is bigger than our medium res by at least 50px, so 
				//resize it 

				int fullWidth = image.Width > FULL_RESOLUTION ? FULL_RESOLUTION : image.Width;
				WebImage fullImage = image.Resize(FULL_RESOLUTION, GetHeightAspectRatio(fullWidth, image));
				retVal.FullImage = fullImage.GetBytes();
			}

			return retVal;
		}

		#endregion

		private static int GetHeightAspectRatio(int newWidth, WebImage image)
		{
			return ((newWidth*image.Height)/image.Width);
		}

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