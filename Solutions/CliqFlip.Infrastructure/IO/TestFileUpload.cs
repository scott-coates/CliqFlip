//using System.Collections.Generic;
//using System.IO;
//using CliqFlip.Infrastructure.IO.Interfaces;

//namespace CliqFlip.Infrastructure.IO
//{
//    public class TestFileUpload : IFileUploadService
//    {
//        public IList<string> UploadFiles(string path, IList<FileToUpload> files)
//        {
//            var retVal = new List<string>();
//            foreach(var file in files)
//            {
//                var saveFilePath = "C:\\test\\" + path + file.Filename;
//                using (FileStream fileStream = File.Create(saveFilePath))
//                {
//                    file.Stream.CopyTo(fileStream);
//                }

//                retVal.Add(saveFilePath);
//            }

//            return retVal;
//        }

//        public void DeleteFiles(string path, IList<string> fileNames)
//        {
			
//        }
//    }
//}