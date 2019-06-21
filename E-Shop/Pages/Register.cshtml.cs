using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Controllers;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using E_Shop.Models.AccountViewModels;
using E_Shop.Models.PersonViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace E_Shop.Pages
{
    
    public class RegModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        [StringLength(100, ErrorMessage = "{0} musí obsahovať aspoň {2} a najviac {1} znakov", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrďte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdenie hesla")]
        [Compare("Password", ErrorMessage = "Zadaná heslá sa nezhodujú")]
        public string ConfirmPassword { get; set; }
    }

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMapper _mapper;
        private IPersonManager _personManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IMapper mapper,
            IPersonManager personManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _personManager = personManager;
        }


        [BindProperty]
        public PersonRegisterViewModel Input { get; set; }
       // public RegModel Input { get; set; }
        

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    AddPerson(Input, user.Id);
                    _logger.LogInformation("User created a new account with password.");
                    this.AddFlashMessage("Váš účet bol úspešne vytvorený", FlashMessageType.Success);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            this.AddFlashMessage("Skontrolujte, či je formulár správne vyplnený. Ak áno, tak účet so zadanou e-mailovou adresou už existuje", FlashMessageType.Danger);
            return Page();
        }

        private void AddPerson(PersonRegisterViewModel PersonVM, string userId = null, int? personId = null)
        {
            //Map from personVM to Person 
            PersonDetail personDetail = _mapper.Map<PersonDetail>(PersonVM);
            Address address = _mapper.Map<Address>(PersonVM);            

            Address deliveryAddress = new Address()
            {
                StreetNameAndHouseNumber = PersonVM.StreetHouseNumberDelivery,
                City = PersonVM.CityDelivery,
                PostalCode = PersonVM.PostalCodeDelivery,
                Country = PersonVM.CountryDelivery
            };

            _personManager.AddPerson(personDetail, address, deliveryAddress, PersonVM.DeliveryAddressIsAddress, userId);
        }

    }

}