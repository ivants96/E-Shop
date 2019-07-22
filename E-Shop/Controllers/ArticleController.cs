using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [PassCartStateFilter]
    [ExceptionsToMessageFilter("Index", "Article")]
    [Authorize]
    public class ArticleController : Controller
    {
        IArticleManager articleManager;

        public ArticleController(IArticleManager articleManager)
        {
            this.articleManager = articleManager;
        }

        // GET: Articles
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(articleManager.GetInfoArticles());
        }

        [AllowAnonymous]
        public IActionResult Career()
        {
            return View(articleManager.GetJobOfferArticles());
        }


        // GET: Article/Details/5
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) { return NotFound(); }
            var model = articleManager.GetArticle(id.Value);
            if (model == null) { return NotFound(); }

            return View(model);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content,ArticleType")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleManager.CreateArticle(article);
                this.AddFlashMessage("Článok bol úspešne pridaný", FlashMessageType.Success);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Article/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var model = articleManager.GetArticle(id.Value);
            if (model == null) { return NotFound(); }

            return View(model);
        }

        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Title,Content,ArticleType")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleManager.Edit(article);
                this.AddFlashMessage("Článok bol úspešne editovaný", FlashMessageType.Success);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }
                        
        public IActionResult Delete(int id)
        {
            articleManager.DeleteArticle(id);
            this.AddFlashMessage("Článok bol úspešne vymazaný", FlashMessageType.Success);
            return RedirectToAction(nameof(Index));
        }
    }
}