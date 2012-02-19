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
			var retVal = new List<string>(files.Count);

			using (AmazonS3 client = AWSClientFactory.CreateAmazonS3Client())
			{
				foreach (FileToUpload file in files)
				{
					var fileUploadRequest = new PutObjectRequest();

					fileUploadRequest
						.WithKey(path + Guid.NewGuid() + Path.GetExtension(file.Filename))
						.WithCannedACL(S3CannedACL.PublicRead)
						.WithBucketName(ConfigurationManager.AppSettings[Constants.S3_BUCKET])
						.WithInputStream(file.Stream);

					fileUploadRequest.AddHeader("Content-Disposition", "attachment; filename = " + file.Filename);

					//"R" - RFC1123 - http://msdn.microsoft.com/en-us/library/az4se3k1.aspx#RFC1123
					fileUploadRequest.AddHeader("Expires", DateTime.UtcNow.AddYears(10).ToString("R"));

					using (S3Response responseWithMetadata = client.PutObject(fileUploadRequest))
					{
						responseWithMetadata.ToString();
					}
				}
			}

			return retVal;
		}

		#endregion
	}
}