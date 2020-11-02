using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace FileProviderSample.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileProvider _fileProvider;

        public FileController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public string GetFile()
        {
            var directoryContents = _fileProvider.GetDirectoryContents("");
            foreach (var directoryContent in directoryContents)
            {
                Console.WriteLine(directoryContent.Name + directoryContent.Length);
            }

            var fileInfo = _fileProvider.GetFileInfo("");
         
            return "ok";
        }

        /// <summary>
        /// 嵌入的资源
        /// </summary>
        /// <returns></returns>
        public string GetEmbeddedFile()
        {
            var directoryContents = _fileProvider.GetFileInfo("File/嵌入的文件.txt");
            using var stream = directoryContents.CreateReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            string file = Encoding.Default.GetString(buffer);
            return file;
        }
    }
}