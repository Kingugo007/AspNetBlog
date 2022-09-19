using Medik.Core.Interfaces;
using Medik.Core.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Medik.Core.Services
{
    public class UploadFiles : IUploadFiles
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadFiles(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string Upload(PostViewModel contents)
        {
            string fileName = string.Empty;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            fileName = Guid.NewGuid().ToString() + "_" + contents.Photo.FileName;
            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                contents.Photo.CopyTo(stream);
            }

            return fileName;
        }
    }
}
