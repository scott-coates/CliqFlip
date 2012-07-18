using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Infrastructure.Images.Interfaces;

namespace CliqFlip.Infrastructure.Images
{
    public class ImageProcessor : IImageProcessor
    {
        private const int MIN_RESOLUTION = 75;
        private const int MAX_RESOLUTION = 15000;
        private const int THUMBNAIL_RESOLUTION = 50;
        private const int MEDIUM_RESOLUTION_WIDTH = 240;
        private const int MEDIUM_RESOLUTION_HEIGHT = 160;
        private const int FULL_RESOLUTION_WIDTH = 640;
        private const int FULL_RESOLUTION_HEIGHT = 428;
        private const string MIN_RESOLUTION_MESSAGE = "The minimum resolution is 75 pixels. Please upload a larger file";
        private const string MAX_RESOLUTION_MESSAGE = "The maximum resolution is 15000 pixels. Please upload a smaller file";
        private static readonly ImageCodecInfo[] _imageCodecs = ImageCodecInfo.GetImageEncoders();
        private static readonly string[] _acceptedExtensions = new[] { "jpg", "jpeg", "tif", "tiff", "png", "bmp", "gif" };

        #region IImageProcessor Members

        public ImageProcessResult ProcessImage(FileStreamDto profileImage)
        {
            var retVal = new ImageProcessResult();

            ValidateExtension(profileImage.FileName);

            using (Image image = Image.FromStream(profileImage.Stream))
            {
                ValidateImageSize(image);

                //We know the image input will always be bigger than thumbnail
                retVal.ThumbnailImage = GetResizedImage(image, THUMBNAIL_RESOLUTION, THUMBNAIL_RESOLUTION);
                retVal.MediumImage = GetResizedImage(image, MEDIUM_RESOLUTION_WIDTH, MEDIUM_RESOLUTION_HEIGHT);

                if ((image.Width >= retVal.MediumImage.Width + 50 && image.Width >= FULL_RESOLUTION_WIDTH)
                    ||
                    (image.Height >= retVal.MediumImage.Height + 50 && image.Height >= FULL_RESOLUTION_HEIGHT))
                {
                    //the + 50 means don't create a full size image if it's barely bigger than a medium sized one

                    retVal.FullImage = GetResizedImage(image, FULL_RESOLUTION_WIDTH, FULL_RESOLUTION_HEIGHT);
                }
            }


            return retVal;
        }

        #endregion

        private ResizedImage GetResizedImage(Image image, int proposedWidth, int proposedHeight)
        {
            ResizedImage retVal;

            int newWidth;
            int newHeight;
            //TODO: the max_height, max_width needs to be more flexible - write unit test around it too
            if (image.Width < proposedWidth && image.Height < proposedHeight)
            {
                //image was smaller than proposed dimensions - use original

                newWidth = image.Width;
                newHeight = image.Height;
            }
            else
            {
                //image was larger than proposed dimensions - resize it

                decimal diffRatio;

                if (image.Width > image.Height)
                {
                    diffRatio = (decimal)proposedWidth / image.Width;
                    newWidth = proposedWidth;
                    decimal tempHeight = image.Height * diffRatio;
                    newHeight = (int)tempHeight;
                }
                else
                {
                    diffRatio = (decimal)proposedHeight / image.Height;
                    newHeight = proposedHeight;
                    decimal tempWidth = image.Width * diffRatio;
                    newWidth = (int)tempWidth;
                }
            }

            using (var resizedBitmap = new Bitmap(newWidth, newHeight))
            {
                using (Graphics newGraphic = Graphics.FromImage(resizedBitmap))
                {
                    retVal = new ResizedImage { Image = new MemoryStream(), Width = newWidth, Height = newHeight };
                    using (var encoderParameters = new EncoderParameters(1))
                    {
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

                        newGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        newGraphic.SmoothingMode = SmoothingMode.HighQuality;
                        newGraphic.CompositingQuality = CompositingQuality.HighQuality;
                        newGraphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        newGraphic.DrawImage(image, 0, 0, newWidth, newHeight);
                        resizedBitmap.Save(retVal.Image, GetImageCodec(image.RawFormat), encoderParameters);

                        //Stream is NOT disposed here - it is sent back as an open stream
                        retVal.Image.Position = 0;
                    }
                }
            }

            return retVal;
        }

        private static void ValidateExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");

            string extension = Path.GetExtension(fileName);

            if (string.IsNullOrWhiteSpace(extension))
                throw new RulesException("image"
                                         , "This file does not contain an extension. Uploaded images require extensions. Ex: myphoto.jpg");

            extension = extension.Substring(1); //get rid of '.'

            if (_acceptedExtensions.All(x => x != extension.ToLower().Trim()))
            {
                throw new RulesException("image", "This is not a valid image type");
            }
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

        private static ImageCodecInfo GetImageCodec(ImageFormat format)
        {
            ImageCodecInfo codec = _imageCodecs.Single(x => x.FormatID == format.Guid);

            if (codec == null)
            {
                throw new ArgumentException("Invalid format value " + format.Guid, "format");
            }

            return codec;
        }
    }
}