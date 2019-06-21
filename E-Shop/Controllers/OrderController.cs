using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Shop.Business.Classes;
using E_Shop.Business.Interfaces;
using E_Shop.Classes;
using E_Shop.Data.Models;
using E_Shop.Extensions;
using E_Shop.Models.OrderViewModels;
using E_Shop.Models.PersonViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shop.Controllers
{
    [ExceptionsToMessageFilter]
    [PassCartStateFilter]
    public class OrderController : Controller
    {
        IOrderManager orderManager;
        IPersonManager personManager;
        private HttpContext httpContext;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> signInManager;

        public OrderController(IOrderManager orderManager,
            IPersonManager personManager,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.orderManager = orderManager;
            this.personManager = personManager;
            this.httpContext = httpContext.HttpContext;
            _mapper = mapper;
            _userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var model = new OrderIndexViewModel()
            {
                OrderItems = orderManager.GetProducts().ToList(),
                OrderSummary = orderManager.GetOrderSummary()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cart(OrderIndexViewModel model)
        {
            orderManager.UpdateCart(httpContext.Request.Form);
            model.OrderItems = orderManager.GetProducts().ToList();
            model.OrderSummary = orderManager.GetOrderSummary();

            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterOrder()
        {
            var order = orderManager.GetOrder();
            var model = new PersonRegisterViewModel();

            if (order.BuyerId.HasValue)
            {
                Person person = personManager.FindById(order.BuyerId.Value);
                model.StreetHouseNumberDelivery = person.DeliveryAddress.StreetNameAndHouseNumber;
                model.CityDelivery = person.DeliveryAddress.City;
                model.PostalCodeDelivery = person.DeliveryAddress.PostalCode;
                model.CountryDelivery = person.DeliveryAddress.Country;
                _mapper.Map(person.PersonDetail, model);
                _mapper.Map(person.Address, model);
            }

            ViewData["orderId"] = order.EOrderId;
            if (order.BuyerId != null)
            {
                ViewData["Buyer"] = order.BuyerId;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterOrder(PersonRegisterViewModel model, bool createAccount = false)
        {

            if (!createAccount)
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = orderManager.GetOrder();
            if (order.BuyerId != null)
            {
                ModelState.Remove("DeliveryAddressIsAddress");
                if (model.DeliveryAddressIsAddress == true && string.IsNullOrEmpty(model.StreetHouseNumberDelivery)
                    && string.IsNullOrEmpty(model.CityDelivery)
                    && string.IsNullOrEmpty(model.PostalCodeDelivery))
                {
                    this.AddFlashMessage("Vyplňte dodávaciu adresu", FlashMessageType.Danger);
                }
                EditPerson(model, order.BuyerId.Value);
                return RedirectToAction("Payment");
            }

            Person person = AddPerson(model);
            orderManager.SetPerson(person);

            if (!createAccount)
            {
                return RedirectToAction("Payment");
            }
            if (createAccount)
            {
                if (ModelState.IsValid)
                {
                    if (_userManager.FindByEmailAsync(model.Email).Result != null)
                    {
                        this.AddFlashMessage(new FlashMessage(string.Format("Email {0} je už registrovaný", model.Email), FlashMessageType.Danger));
                    }

                    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        person.User = user;
                        personManager.InsertOrEdit(person);
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Payment");
                    }
                    else
                    {
                        string allErrors = "Registrácia sa nepodarila \n";
                        foreach (var error in result.Errors)
                        {
                            allErrors += error.Description + " \n";
                            this.AddFlashMessage(allErrors, FlashMessageType.Danger);
                            return RedirectToAction("RegisterOrder");
                        }
                    }
                }
            }
            return RedirectToAction("Payment");
        }

        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            var order = orderManager.GetOrder();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                orderManager.SetPerson(currentUser.Person);
            }
            else if (order.BuyerId == null)
            {
                return RedirectToAction("RegisterOrder");
            }

            var model = new OrderPaymentViewModel()
            {
                TransportationMethods = new SelectList(orderManager.GetTransportMethods(), "Key", "Value"),
                WaysOfPayment = new SelectList(orderManager.GetPaymentMethods(), "Key", "Value")

            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Payment(int transportMethodId, int paymentMethodId)
        {
            orderManager.SetTransportMethod(transportMethodId);
            orderManager.SetPaymentMethod(paymentMethodId);
            return RedirectToAction("Summary");
        }

        [HttpGet]
        public IActionResult Summary()
        {
            var order = orderManager.GetOrder(create: false);
            if (order.BuyerId == null)
            {
                return RedirectToAction("RegisterOrder");
            }
            if (order.DeliveryProduct == null || order.WayOfPayment == null)
            {
                return RedirectToAction("Payment");
            }

            var orderItems = orderManager.GetProducts(order.EOrderId);
            var orderSummary = orderManager.GetOrderSummary(order.EOrderId);

            var transportMethod = new OrderItemInfo()
            {
                ProductId = order.DeliveryProductId.Value,
                Price = order.DeliveryProduct.Price,
                Quantity = 1,
                Url = null,
                Title = order.DeliveryProduct.Title
            };
            var paymentMethod = new OrderItemInfo()
            {
                ProductId = order.WayOfPaymentId.Value,
                Quantity = 1,
                Price = 0,
                Url = null,
                Title = order.WayOfPayment.Title
            };

            OrderSummaryViewModel model = new OrderSummaryViewModel()
            {
                OrderItems = orderItems,
                OrderSummary = orderSummary,
                WayOfPayment = paymentMethod,
                TransportMethod = transportMethod,
                PersonDetail = order.BuyerPersonDetail,
                Address = order.BuyerAddress,
                DeliveryAddress = order.BuyerDeliveryAddress,
                OrderId = order.EOrderId,
                Registered = order.Buyer.User != null
            };

            model.OrderSummary.Price += transportMethod.Price;

            return View(model);
        }

        [HttpPost]
        public IActionResult CompleteOrder()
        {
            orderManager.CompleteOrder();
            return RedirectToAction("Index", "Home");
        }

        private Person AddPerson(PersonRegisterViewModel PersonVM)
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

            return personManager.AddPerson(personDetail, address, deliveryAddress, PersonVM.DeliveryAddressIsAddress);
        }

        private void EditPerson(PersonRegisterViewModel PersonVM, int personId)
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

            personManager.EditPerson(personDetail, address, deliveryAddress, personId: personId);
        }



    }
}