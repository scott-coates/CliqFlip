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
			if (!string.IsNullOrWhiteSpace(path) && !path.EndsWith("/"))
				throw new ArgumentException("Invalid path. Path must end in a forward slash: " + path);

			var retVal = new List<string>(files.Count);

			using (AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(new AmazonS3Config().WithServiceURL("s3-us-west-1.amazonaws.com")))
			{
				var bucketName = ConfigurationManager.AppSettings[Constants.S3_BUCKET];

				foreach (FileToUpload file in files)
				{
					var fileUploadRequest = new PutObjectRequest();

					var key = path + Guid.NewGuid() + Path.GetExtension(file.Filename);

					fileUploadRequest
						.WithKey(key)
						.WithCannedACL(S3CannedACL.PublicRead)
						.WithBucketName(bucketName)
						.WithInputStream(file.Stream);

					fileUploadRequest.StorageClass = S3StorageClass.ReducedRedundancy;

					//"R" - RFC1123 - http://msdn.microsoft.com/en-us/library/az4se3k1.aspx#RFC1123
					fileUploadRequest.AddHeader("Expires", DateTime.UtcNow.AddYears(10).ToString("R"));
					fileUploadRequest.AddHeader("Cache-Control", "public, max-age=31536000");

					using (S3Response responseWithMetadata = client.PutObject(fileUploadRequest))
					{
						responseWithMetadata.ToString();
					}

					retVal.Add(string.Format("http://{0}/{1}", bucketName, key));
				}
			}

			return retVal;
		}

		#endregion
	}
}