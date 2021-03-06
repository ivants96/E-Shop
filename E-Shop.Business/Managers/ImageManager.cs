﻿using E_Shop.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Web.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace E_Shop.Business.Managers
{
   public class ImageManager : IImageManager
    {
        public enum ImageExtension { Bmp, Gif, Jpeg, Png }

        public string OutputDirectoryPath { get; private set; }

        public ImageManager(string outputDirectoryPath) => this.OutputDirectoryPath = outputDirectoryPath;

        /// <summary>
        /// Obaľujúca metóda pre uloženie obrázku zo vstupu.
        /// </summary>
        /// <param name="file">Nezpracovaný súbor s obrázkom k uloženiu.</param>
        /// <param name="fileName">Názov súboru po uložení (bez prípony).</param>
        /// <param name="extension">Formát, v ktorom sa má obrázok uložiť.</param>
        /// <param name="width">Šířka obrázku po uložení (nepovinný parameter).</param>
        /// <param name="height">Výška obrázku po uložení (nepovinný parameter).</param>
        public void SaveImage(IFormFile file, string fileName, ImageExtension extension, int width = 0, int height = 0)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var img = Image.Load(stream.ToArray());

                // ImageSharp defaultne zachová pomer strán, ak je niektorý rozmer == 0
                ResizeImage(img, width, height);

                IImageEncoder encoder;
                switch (extension)
                {
                    case ImageExtension.Bmp:
                        encoder = new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder();
                        fileName += ".bmp";
                        break;
                    case ImageExtension.Gif:
                        encoder = new SixLabors.ImageSharp.Formats.Gif.GifEncoder();
                        fileName += ".gif";
                        break;
                    case ImageExtension.Jpeg:
                        encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder();
                        fileName += ".jpeg";
                        break;
                    case ImageExtension.Png:
                        encoder = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                        fileName += ".png";
                        break;
                    default:
                        return;
                }

                img.Save(OutputDirectoryPath + fileName, encoder);
            }
        }

        private Image<Rgba32> ResizeImage(Image<Rgba32> image, int width = 0, int height = 0)
        {
            // ImageSharp defaultne zachová pomer strán, ak je niektorý rozmer == 0
            if (width > 0 || height > 0)
            {
                image.Mutate(x => x.Resize(
                                            new ResizeOptions() { Size = new SixLabors.Primitives.Size(width, height) }
                                            ));
            }

            return image;
        }

        public void ResizeImage(string path, int width = 0, int height = 0) => ResizeImage(Image.Load(path), width, height).Save(path);

        public static IServiceCollection ConfigureImageProcessingMiddleWare(IServiceCollection services)
        {
            services.AddImageSharp();
            return services;
        }
    }
}
