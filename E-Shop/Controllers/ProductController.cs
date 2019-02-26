using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
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
            model.AvailableCategories = categoryManager.GetLeaves();
            return View(model);
        }


    }
}