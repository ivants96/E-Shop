using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Classes;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace E_Shop.Pages
{
   
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Boli ste odlhásený.");
            if (returnUrl != null)
            {
                this.AddFlashMessage("Boli ste úspešne odhlásený.", FlashMessageType.Success);
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}