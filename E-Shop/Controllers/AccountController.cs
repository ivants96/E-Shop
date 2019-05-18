using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using E_Shop.Models.PersonViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private IPersonManager _personManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPersonManager personManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _personManager = personManager;
        }

        [HttpGet]
        public IActionResult Profile()
        {

            var person = _personManager.FindByUserId(_userManager.FindByNameAsync(User.Identity.Name).Result.Id);
            PersonEditViewModel model = new PersonEditViewModel()
            {
                StreetHouseNumberDelivery = person.DeliveryAddress.StreetNameAndHouseNumber,
                CityDelivery = person.DeliveryAddress.City,
                PostalCodeDelivery = person.DeliveryAddress.PostalCode,
                CountryDelivery = person.DeliveryAddress.Country
            };

            //Map from person to PersonEditViewModel
            _mapper.Map(person.Address, model);
            _mapper.Map(person.PersonDetail, model);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var person = _personManager.FindByUserId(_userManager.FindByNameAsync(User.Identity.Name).Result.Id);
            PersonEditViewModel model = new PersonEditViewModel()
            {
                StreetHouseNumberDelivery = person.DeliveryAddress.StreetNameAndHouseNumber,
                CityDelivery = person.DeliveryAddress.City,
                PostalCodeDelivery = person.DeliveryAddress.PostalCode,
                CountryDelivery = person.DeliveryAddress.Country
            };

            //Map from person to PersonEditViewModel
            _mapper.Map(person.Address, model);
            _mapper.Map(person.PersonDetail, model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PersonEditViewModel PersonVM)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;                
                EditPerson(PersonVM);
                this.AddFlashMessage(new FlashMessage("Údaje boli úspešne zmenené.", FlashMessageType.Success));
                return RedirectToAction("Profile", "Account");
            }

            return View(PersonVM);
        }


        private void EditPerson(PersonEditViewModel model)
        {
            var userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            Person person = _personManager.FindByUserId(userId);

            //Map from personVM to Person 
            PersonDetail personDetail = _mapper.Map<PersonDetail>(model);
            Address address = _mapper.Map<Address>(model);

            Address deliveryAddress = new Address()
            {
                StreetNameAndHouseNumber = model.StreetHouseNumberDelivery,
                City = model.CityDelivery,
                PostalCode = model.PostalCodeDelivery,
                Country = model.CountryDelivery
            };

            _personManager.EditPerson(personDetail, address, deliveryAddress, userId);
        }
    }
}