using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace E_Shop.Controllers
{
    [ExceptionsToMessageFilter]
    [PassCartStateFilter]
    public class OrderController : Controller
    {
        IOrderManager orderManager;
        IPersonManager personManager;
        private IRazorViewEngine razorViewEngine;
        private ITempDataProvider tempDataProvider;
        private HttpContext httpContext;
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IServiceProvider serviceProvider;

        public OrderController(IOrderManager orderManager,
            IPersonManager personManager,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider serviceProvider)
        {
            this.orderManager = orderManager;
            this.personManager = personManager;
            this.httpContext = httpContext.HttpContext;
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            _mapper = mapper;
            _userManager = userManager;
            this.signInManager = signInManager;
            this.serviceProvider = serviceProvider;
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterOrder(PersonRegisterViewModel model, bool createAccount = false)
        {
            // in case that person doesn't want to register while filling out order details
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
            if (order.BuyerId != null) // if user clicks back on RegisterOrder and edits order details
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
            httpContext.Session.SetString("SummaryViewModel", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder()
        {
            var model = JsonConvert.DeserializeObject<OrderSummaryViewModel>(httpContext.Session.GetString("SummaryViewModel"));
            orderManager.CompleteOrder(await RenderToStringAsync("_SummaryPartial", model));
            this.AddFlashMessage("Ďakujeme za Váš nákup! Na e-mail Vám bolo odoslané povtrdenie objednávky", FlashMessageType.Success);
            return RedirectToAction("Index", "Home");
        }

        private string ConvertViewToString(string viewName, object model)
        {
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"Nepodarilo sa nájsť view s názvom {viewName}");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext).Wait();
                return sw.ToString();
            }
        }

        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext() { RequestServices = serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
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