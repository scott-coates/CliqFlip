using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Images.Interfaces;

namespace CliqFlip.Infrastructure.Images
{
	public class ImageProcessor : IImageProcessor
	{
		private const int MIN_RESOLUTION = 100;
		private const int MAX_RESOLUTION = 2048;
		private const int THUMBNAIL_RESOLUTION = 50;
		private const int MEDIUM_RESOLUTION_WIDTH = 240;
		private const int MEDIUM_RESOLUTION_HEIGHT = 160;
		private const int FULL_RESOLUTION_WIDTH = 640;
		private const int FULL_RESOLUTION = 428;
		private const string MIN_RESOLUTION_MESSAGE = "The minimum resolution is 100 pixels. Please upload a larger file";
		private const string MAX_RESOLUTION_MESSAGE = "The maximum resolution is 2048 pixels. Please upload a smaller file";
		private static readonly ImageCodecInfo[] _imageCodecs = ImageCodecInfo.GetImageEncoders();

		#region IImageProcessor Members

		public ImageProcessResult ProcessImage(HttpPostedFileBase profileImage)
		{
			var retVal = new ImageProcessResult();

			using (Image image = Image.FromStream(profileImage.InputStream))
			{
				ValidateImageSize(image);

				//We know the image input will always be bigger than thumbnail
				retVal.ThumbnailImage = GetResizedImage(image, THUMBNAIL_RESOLUTION, THUMBNAIL_RESOLUTION);
			}


			//int mediumWidth = image.Width > MEDIUM_RESOLUTION ? MEDIUM_RESOLUTION : image.Width;


			//WebImage mediumImage = image.Clone().Resize(MEDIUM_RESOLUTION, GetHeightAspectRatio(mediumWidth, image));
			//retVal.MediumImage = mediumImage.GetBytes();

			//int fullWidth = image.Width > FULL_RESOLUTION ? FULL_RESOLUTION : image.Width;
			//if (fullWidth >= (MEDIUM_RESOLUTION + 50))
			//{
			//    //the + 50 means don't create a full size image if it's barely bigger than a medium sized one

			//    //the image res is bigger than our medium res by at least 50px, so 
			//    //resize it 

			//    WebImage fullImage = image.Clone().Resize(FULL_RESOLUTION, GetHeightAspectRatio(fullWidth, image));
			//    retVal.FullImage = fullImage.GetBytes();
			//}

			return retVal;
		}

		#endregion

		private byte[] GetResizedImage(Image image, int proposedWidth, int proposedHeight)
		{
			byte[] retVal = null;

			int newWidth;
			int newHeight;

			if (image.Width >= proposedWidth || image.Height >= proposedHeight)
			{
				newWidth = image.Width;
				newHeight = image.Height;
				//image was smaller than proposed dimensions
			}
			else
			{
				//image was larger than proposed dimensions

				decimal diffRatio;
				if (image.Width > image.Height)
				{
					diffRatio = (decimal) proposedWidth/image.Width;
					newWidth = proposedWidth;
					decimal tempHeight = image.Height*diffRatio;
					newHeight = (int) tempHeight;
				}
				else
				{
					diffRatio = (decimal) proposedHeight/image.Height;
					newHeight = proposedHeight;
					decimal tempWidth = image.Width*diffRatio;
					newWidth = (int) tempWidth;
				}
			}

			using (var resizedBitmap = new Bitmap(newWidth, newHeight))
			{
				using (Graphics newGraphic = Graphics.FromImage(resizedBitmap))
				{
					using (var resultStream = new MemoryStream())
					{
						using (var encoderParameters = new EncoderParameters(1))
						{
							encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

							newGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
							newGraphic.SmoothingMode = SmoothingMode.HighQuality;
							newGraphic.CompositingQuality = CompositingQuality.HighQuality;
							newGraphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
							newGraphic.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
							
							newGraphic.DrawImage(image, 0, 0, newWidth, newHeight);
							
							resizedBitmap.Save(resultStream, _imageCodecs[1], encoderParameters);
							retVal = resultStream.ToArray();
						}
					}
				}
			}
			

			return retVal;
		}

		private static void ValidateImageSize(Image image)
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