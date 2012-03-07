using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CliqFlip.Domain.Common;
using CliqFlip.Infrastructure.IO.Interfaces;

namespace CliqFlip.Infrastructure.IO
{
	public class S3FileUploadService : IFileUploadService
	{
		#region IFileUploadService Members

		public IList<string> UploadFiles(string path, IList<FileToUpload> files)
		{
			ValidatePath(path);

			var retVal = new List<string>(files.Count);

			using (AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(new AmazonS3Config().WithServiceURL("s3-us-west-1.amazonaws.com")))
			{
				string bucketName = ConfigurationManager.AppSettings[Constants.S3_BUCKET];

				foreach (FileToUpload file in files)
				{
					var fileUploadRequest = new PutObjectRequest();

					string key = path + Guid.NewGuid() + Path.GetExtension(file.Filename);

					fileUploadRequest
						.WithKey(key)
						.WithCannedACL(S3CannedACL.PublicRead)
						.WithBucketName(bucketName)
						.WithMetaData("Image-Info",file.MetaInfo)
						.WithInputStream(file.Stream);

					fileUploadRequest.StorageClass = S3StorageClass.ReducedRedundancy;

					//"R" - RFC1123 - http://msdn.microsoft.com/en-us/library/az4se3k1.aspx#RFC1123
					fileUploadRequest.AddHeader("Expires", DateTime.UtcNow.AddYears(10).ToString("R"));
					fileUploadRequest.AddHeader("Cache-Control", "public, max-age=31536000");

					using (client.PutObject(fileUploadRequest))
					{
					}

					retVal.Add(string.Format("http://{0}/{1}", bucketName, key));
				}
			}

			return retVal;
		}

		public void DeleteFiles(string path, IList<string> fileNames)
		{
			ValidatePath(path);
			using (AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(new AmazonS3Config().WithServiceURL("s3-us-west-1.amazonaws.com")))
			{
				string bucketName = ConfigurationManager.AppSettings[Constants.S3_BUCKET];

				foreach (string file in fileNames)
				{
					var fileDeleteRequest = new DeleteObjectRequest();

					string key = path + Path.GetFileName(file);

					fileDeleteRequest
						.WithKey(key)
						.WithBucketName(bucketName);

					using (client.DeleteObject(fileDeleteRequest))
					{
					}
				}
			}
		}

		#endregion

		private static void ValidatePath(string path)
		{
			if (!string.IsNullOrWhiteSpace(path) && !path.EndsWith("/"))
				throw new ArgumentException("Invalid path. Path must end in a forward slash: " + path);
		}
	}
}