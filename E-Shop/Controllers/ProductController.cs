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
using E_Shop.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
                if (model.Product == default(Product))
                {
                    throw new NullReferenceException("Produkt nebol nájdený");
                }
            }
            model.AvailableCategories = categoryManager.GetLeaves();
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
            int[] selectedCategories = AllCategories.Where(cat => model.PostedCategories[AllCategories.IndexOf(cat)])
                                                        .Select(cat => cat.CategoryId)  // z každéj kategorie nás zaujíma len jej ID
                                                        .ToArray();
            // uloženie produktu aj s jeho väzbami                                    
            productManager.SaveProduct(model.Product);
            categoryManager.UpdateProductCategories(model.Product.ProductId, selectedCategories);
            productManager.SaveProductImages(model.Product, model.UploadedImages);

            this.AddFlashMessage("Produkt bol úspešne pridaný", FlashMessageType.Success);
            return RedirectToAction("Manage");
        }

        public IActionResult Delete(int id)
        {
            var product = productManager.FindProductById(id);
            productManager.DeleteProduct(id);
            this.AddFlashMessage("Produkt bol úspešne odstránený", FlashMessageType.Success);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Clear(int id)
        {
            var product = productManager.FindProductById(id);
            productManager.ClearProductCategories(id);
            this.AddFlashMessage("Produkt bol upravený", FlashMessageType.Success);
            return RedirectToAction("Manage", new { url = product.Url });
        }

        [Authorize(Roles = "Admin")]
        public void DeleteImage(int productID, int imageIndex)
        {
            productManager.RemoveProductImage(productID, imageIndex);
        }

        const int pageSize = 6;

        public IActionResult Index(int? id, string searchPhrase, int? page, ProductIndexViewModel model)
        {
            //ToPagedList (1, pageSize) - return 1 of n pages, one page can contain number set in pageSize at most

            //id = categoryId, click on category in menu
            if (id.HasValue)
            {
                searchPhrase = string.Empty;
                model.Products = productManager.FindByCategoryId(id.Value).ToPagedList(1, pageSize);
                model.CurrentPhrase = string.Empty;
                model.CurrentCategoryId = id;
            }
            else if (searchPhrase != null)  //search product in search form
            {
                model.Products = productManager.SearchProducts(searchPhrase).ToPagedList(1, pageSize);
                model.CurrentPhrase = searchPhrase;
                model.CurrentCategoryId = null;
            }
            else //filtering or sorting products or click on next page
            {
                searchPhrase = model.CurrentPhrase;
                model.Products = productManager.SearchProducts(
                    model.CurrentPhrase,
                    model.CurrentCategoryId,
                    model.SortCriteria ?? "rating",
                    model.StartPrice ?? 0,
                    model.EndPrice ?? 0,
                    model.InStock)
                    .ToPagedList(page ?? 1, pageSize);
            }
            ViewData["SearchPhrase"] = searchPhrase;
            return View(model);
        }

        public IActionResult ProcessStockForm(int productId, int quantity)
        {
            productManager.AddToStock(productId, quantity);
            this.AddFlashMessage("Počet produktov na sklade bol zmenený", FlashMessageType.Success);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Detail(string url)
        {
            var model = new ProductDetailViewModel()
            {
                Product = productManager.FindProductByUrl(url)
            };
            return View(model);
        }

    }
}