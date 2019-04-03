using E_Shop.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IProductManager
    {
        Product FindProductById(int id);
        Product FindProductByUrl(string url);
        void SaveProduct(Product product);
        void ClearProductCategories(int id);
        void DeleteProduct(int id);
        void SaveProductImages(Product product, List<IFormFile> images);
        void RemoveProductImage(int productId, int imageIndex);
        List<Product> FindByCategoryId(int categoryId);
        List<Product> SearchProducts(string searchPhrase);
        List<Product> SearchProducts
            (
            string searchPhrase,
            int? categoryId = null,
            string orderBy = "rating",
            decimal startPrice = 0,
            decimal endPrice = 0,
            bool inStock = false
            );
        void AddToStock(int productId, int quantity);



    }
}
