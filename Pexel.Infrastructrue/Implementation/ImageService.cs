using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Pexel.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IFileProvider _fileProvider;

        public ImageService(IFileProvider file)
        {
            _fileProvider  = file;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection file, string src)
        {
            var saveImage = new List<string>();
            var imageDirctory = Path.Combine("wwwroot", "Images",src);

            if(Directory.Exists(imageDirctory) is not true)
            { 
                Directory.CreateDirectory(imageDirctory);   
            }

            foreach(var item in file) 
            {
                if (item.Length > 0) 
                {
                    var imageName = item.FileName;
                    var imageSrc = $"Images/{src}/{imageName}";
                    var root = Path.Combine(imageDirctory, imageName);

                    using (FileStream stream = new FileStream(root,FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    saveImage.Add(imageSrc);
                }
            }

            return saveImage;
        }
        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);

        }
    }
}
