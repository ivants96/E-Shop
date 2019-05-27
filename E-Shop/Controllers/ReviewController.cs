using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [ExceptionsToMessageFilter]
    [PassCartStateFilter]
    public class ReviewController : Controller
    {
        IReviewManager reviewManager;
        private UserManager<ApplicationUser> userManager;

        public ReviewController(IReviewManager reviewManager, UserManager<ApplicationUser> userManager)
        {
            this.reviewManager = reviewManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddReview(string productUrl)
        {
            return RedirectToActionPermanent("Detail", "Product", new { url = productUrl });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserId = userManager.GetUserId(HttpContext.User);
                reviewManager.AddReview(review);
                this.AddFlashMessage("Recenzia bola úspešne pridaná", FlashMessageType.Success);
            }
            //return RedirectToAction("Detail", "Product", new { url = review.Product.Url });
            return Redirect(ControllerContext.HttpContext.Request.Headers["Referer"].ToString());
        }




    }
}