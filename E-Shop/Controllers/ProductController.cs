using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using E_Shop.Extensions;
using E_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [ExceptionsToMessageFilter]
    public class ProductController : Controller
    {
        IProductManager productManager;
        ICategoryManager categoryManager;

        public ProductController(IProductManager productManager, ICategoryManager categoryManager)
        {
            this.productManager = productManager;
            this.categoryManager = categoryManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Manage(string url)
        {
            var model = new ManageProductViewModel()
            {
                FormCaption = string.IsNullOrEmpty(url) ? "Nový produkt" : "Editácia produktu"
            };

            if (string.IsNullOrEmpty(url))
            {
                model.Product = new Product();
            }
            else
            {
                model.Product = productManager.FindProductByUrl(url);
                if(model.Product == default(Product))
                {
                    throw new NullReferenceException("Produkt nebol nájdený");
                }
            }
            model.AvailableCategories = categoryManager.GetLeaves().ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Manage(ManageProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Product.ProductId == 0 ? "Nový produkt" : "Editácia produktu";
                model.AvailableCategories = categoryManager.GetLeaves();
                this.AddFlashMessage("Zlé parametre produktu!", FlashMessageType.Danger);
                return View(model);
            }

            var AllCategories = categoryManager.GetLeaves();

            // najdi ze všech dostupných kategorií ty, které jsou označené (PostedCategoried[index] == true)
            int[] selectedCategories = AllCategories.Where(c => model.PostedCategories[AllCategories.IndexOf(c)])
                                                        .Select(c => c.CategoryId)  // z každé kategorie nás zajímá jen její ID
                                                        .ToArray();

            int oldProductID = model.Product.ProductId;
            int oldImagesCount = model.Product.ImagesCount;

            // uložení produktu i s jeho vazbami
            productManager.SaveProduct(model.Product);
            categoryManager.UpdateProductCategories(model.Product.ProductId, selectedCategories);
            //productManager.SaveProductImages(model.Product, model.UploadedImages, oldProductID, oldImagesCount);

            this.AddFlashMessage("Produkt bol úspešne pridaný", FlashMessageType.Success);
            return RedirectToAction("Manage");
        }

    }
}