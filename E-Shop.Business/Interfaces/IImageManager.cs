using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static E_Shop.Business.Managers.ImageManager;

namespace E_Shop.Business.Interfaces
{
    interface IImageManager
    {
        void SaveImage(IFormFile file, string fileName, ImageExtension extension, int width = 0, int height = 0);
        void ResizeImage(string path, int width = 0, int height = 0);
    }
}
