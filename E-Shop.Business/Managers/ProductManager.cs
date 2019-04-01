using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class ProductManager : IProductManager
    {
        private const string ProductImagesPath = "wwwroot/images/products/";
        private IImageManager imageManager = new ImageManager(ProductImagesPath);
        private const int ProductImageMaxHeight = 400;
        private const int ProductThumbnailSize = 320;

        private IProductRepository productRepository;
        private ICategoryProductRepository categoryProductRepository;

        public ProductManager(IProductRepository productRepository, ICategoryProductRepository categoryProductRepository)
        {
            this.productRepository = productRepository;
            this.categoryProductRepository = categoryProductRepository;
        }

        public Product FindProductById(int id)
        {
            return productRepository.FindById(id);
        }

        public Product FindProductByUrl(string url)
        {
            return productRepository.FindByUrl(url);
        }

        public void SaveProduct(Product product)
        {
            productRepository.Update(product);
        }


        public void ClearProductCategories(int id)
        {
            var product = FindProductById(id);
            product.CategoryProducts.Clear();
            productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var product = FindProductById(id);

            int imagesCount = product.ImagesCount;
            RemoveThumbnailFile(product.ProductId);// odstranění thumbnailu
            for (int i = 0; i < imagesCount; i++)// odstránenie obrázkov
            {
                RemoveImageFile(product.ProductId, i);
            }
            product.CategoryProducts.Clear();
            productRepository.Delete(id);

        }

        public void SaveProductImages(Product product, List<IFormFile> images)
        {
            int imagesCount = 0;

            // nahrávanie ďalších obrázkov 
            if (images != null)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    if (images[i] == null || !images[i].ContentType.ToLower().Contains("image")) { continue; }

                    imageManager.SaveImage(images[i],
                                            GetImageFileName(product.ProductId, imagesCount, full: false),
                                            ImageManager.ImageExtension.Jpeg,
                                            height: ProductImageMaxHeight);

                    // prvý obrázok uložíme ako miniatúru
                    if (imagesCount == 0)
                    {
                        imageManager.SaveImage(images[i],
                                                GetThumbnailFileName(product.ProductId, full: false),
                                                ImageManager.ImageExtension.Png,
                                                ProductThumbnailSize);
                    }

                    imagesCount++;
                }

                product.ImagesCount = imagesCount;
                productRepository.Update(product);
            }
            else { throw new Exception("Nepodarilo sa nahrať obrázky!"); }
        }

        public void RemoveProductImage(int productId, int imageIndex)
        {
            Product product = productRepository.FindById(productId);
            // Ak je to prvý obrázok, zmažeme aj miniatúru
            if (imageIndex == 0)
            {
                RemoveThumbnailFile(productId);
                // Snažíme se vytvoriť novú miniatúru z druhého obrázku
                string secondImagePath = GetImageFileName(productId, 1);
                if (File.Exists(secondImagePath))
                {
                    string thumbFileName = GetThumbnailFileName(product.ProductId);
                    imageManager.ResizeImage(thumbFileName, ProductThumbnailSize);
                }
            }

            // Mažeme obrázok
            RemoveImageFile(productId, imageIndex);

            // Aktualizácia obrázkov produktu
            product.ImagesCount--;
            productRepository.Update(product);
        }

        private string GetImageFileName(int productId, int imageIndex, bool full = true)
        {
            string result = $"{productId}_{imageIndex}";
            if (full) { result = ProductImagesPath + result + ".jpeg"; }
            return result;
        }

        private string GetThumbnailFileName(int productId, bool full = true)
        {
            string result = $"{productId}_thumb";
            if (full) { result = ProductImagesPath + result + ".png"; }
            return result;
        }

        private void RemoveImageFile(int productId, int imageIndex)
        {
            string fileName = GetImageFileName(productId, imageIndex);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private void RemoveThumbnailFile(int productId)
        {
            string thumbFileName = GetThumbnailFileName(productId);
            if (File.Exists(thumbFileName))
            {
                File.Delete(thumbFileName);
            }
        }

        public List<Product> FindByCategoryId(int categoryId)
        {
            return productRepository.FindByCategoryId(categoryId);
        }

        public List<Product> SearchProducts(string searchPhrase)
        {
            return productRepository.SearchProducts(searchPhrase);
        }

        public List<Product> SearchProducts(string searchPhrase, int? categoryId = null, string orderBy = "rating", decimal startPrice = 0, decimal endPrice = 0, bool inStock = false)
        {
            var result = SearchProducts(searchPhrase);
            
            if (categoryId.HasValue)
            {
                result = result.Where(p => p.CategoryProducts
               .Select(cp => cp.CategoryId)
               .Contains(categoryId.Value))
               .ToList();
            }
            if (startPrice > 0)
            {
                result = result.Where(p => p.Price >= startPrice).ToList();
            }
            if (endPrice > 0)
            {
                result = result.Where(p => p.Price <= endPrice).ToList();
            }
            if (inStock)
            {
                result = result.Where(p => p.Stock > 0).ToList();
            }

            switch (orderBy.ToLower())
            {
                case "lowest_price":
                    result = result.OrderBy(p => p.Price).ToList();
                    break;
                case "highest_price":
                    result = result.OrderByDescending(p => p.Price).ToList();
                    break;
                case "newest":
                    result = result.OrderByDescending(p => p.ProductId).ToList();
                    break;
                default:
                    result = result.OrderByDescending(p => p.Rating)
                        .ThenByDescending(p => p.ProductId)
                        .ToList();
                    break;

            }
            return result;
        }





    }
}
