﻿using E_Shop.Business.Classes;
using E_Shop.Business.Extensions;
using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class OrderManager : IOrderManager
    {
        private IProductRepository _productRepository;
        private IProductEOrderRepository _productEOrderRepository;
        private IPersonRepository _personRepository;
        private IEOrderRepository _eOrderRepository;
        private ICategoryRepository _categoryRepository;
        private readonly HttpContext httpContext;
        private IEmailSender emailSender;

        public OrderManager
            (
            IProductRepository productRepository,
            IProductEOrderRepository productEOrderRepository,
            IEOrderRepository eOrderRepository,
            ICategoryRepository categoryRepository,
            IPersonRepository personRepository,
            IHttpContextAccessor context,
            IEmailSender emailSender
            )
        {
            _productRepository = productRepository;
            _productEOrderRepository = productEOrderRepository;
            _eOrderRepository = eOrderRepository;
            _categoryRepository = categoryRepository;
            _personRepository = personRepository;
            httpContext = context.HttpContext;
            this.emailSender = emailSender;
        }

        public EOrder CreateOrder()
        {
            string token = Guid.NewGuid().ToString();
            var order = new EOrder()
            {
                Token = token,
                Created = DateTime.UtcNow,
                OrderState = OrderState.CREATED
            };
            _eOrderRepository.Add(order);
            // add cookie to browser
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["order_id"] = order.EOrderId.ToString();
            queryString["token"] = token;

            var cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.Add(TimeSpan.FromDays(701))
            };
            httpContext.Response.Cookies.Append("order", JsonConvert.SerializeObject(queryString, Formatting.Indented), cookieOptions);
            return order;
        }

        public EOrder GetOrder(int? orderId = null, bool create = true)
        {
            if (orderId.HasValue)
            {
                return _eOrderRepository.FindById(orderId.Value);
            }

            int? attemptRetrieve = httpContext.Session.GetInt32("orderId");
            if (attemptRetrieve.HasValue)
            {
                return _eOrderRepository.FindById(attemptRetrieve.Value);
            }

            int id = 0;
            string token = null;
            string fromCookie = httpContext.GetCookie("order");
            if (fromCookie != string.Empty)
            {
                NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(fromCookie);
                int.TryParse(queryString.Get("order_id"), out id);
                token = queryString.Get("token");
            }

            if (!create && id == 0) { return null; }
            // Find order from cookie(fromCookie above) sent by user
            var order = _eOrderRepository.FindOrderIdByTokenState(id, token, OrderState.CREATED);

            // if order wasn't found create new one
            if (order == null)
            {
                order = CreateOrder();
            }

            httpContext.Session.SetInt32("orderId", order.EOrderId);
            return order;
        }

        public bool IsProductAvailable(int productId)
        {
            var product = _productRepository.FindById(productId);
            if (product == null)
            {
                return false;
            }

            return !product.Hidden && product.CategoryProducts.Any(c => !c.Category.Hidden) && product.Stock > 0;
        }

        public void AddProducts(int productId, int quantity, int? orderId = null, bool ignoreHiddenProducts = false)
        {
            // If orderId == null We're working with current Order
            var order = GetOrder(orderId);
            if (quantity <= 0)
            {
                throw new Exception("Nemožno vložiť záporný alebo nulový počet kusov");
            }

            if (!ignoreHiddenProducts && !IsProductAvailable(productId))
            {
                throw new Exception("Produkt nie je dostupný");
            }

            var item = _productEOrderRepository.FindByOrderIdProductId(order.EOrderId, productId);
            if (item == null)
            {
                // Pridáť do košíku
                _productEOrderRepository.Add(new ProductEOrder
                {
                    Quantity = quantity,
                    ProductId = productId,
                    EOrderId = order.EOrderId
                });
            }
            else
            {
                // If cart alredy contains product don't add it again just change quantity
                item.Quantity += quantity;
                _productEOrderRepository.Update(item);
            }
        }

        public OrderSummary GetOrderSummary(int? orderId = null)
        {
            var order = GetOrder(orderId, false);

            if (order == null)
            {
                return new OrderSummary() { Price = 0, Quantity = 0 };
            }
            var items = _productEOrderRepository.FindByOrderId(order.EOrderId).ToList();
            var result = new OrderSummary()
            {
                Price = items.Sum(i => i.Quantity * i.Product.Price),
                Quantity = items.Sum(i => i.Quantity)
            };
            return result;
        }

        // Displays list of products in the cart
        public List<OrderItemInfo> GetProducts(int? orderId = null)
        {
            var order = GetOrder(orderId);
            if (order == null)
            {
                return new List<OrderItemInfo>();
            }
            return _productEOrderRepository.FindByOrderId(order.EOrderId)
                .Select(x => new OrderItemInfo()
                {
                    ProductId = x.ProductId,
                    Price = x.Product.Price,
                    Quantity = x.Quantity,
                    Url = x.Product.Url,
                    Title = x.Product.Title
                }).ToList();
        }

        public void UpdateProductInOrder(int orderId, int productId, int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception("Počet produktov nesmie byť záporný");
            }

            var item = _productEOrderRepository.FindByOrderIdProductId(orderId, productId);
            if (quantity == 0)
            {
                _productEOrderRepository.Delete(item.ProductEOrderId);
            }
            else
            {
                item.Quantity = quantity;
                _productEOrderRepository.Update(item);
            }
        }

        public void UpdateCart(IFormCollection form)
        {
            var order = GetOrder();
            foreach (var key in form.Keys)
            {
                if (!key.StartsWith("quantity_")) // input for quantity will have prefix _quantity@item.ProductId in it's name attribute
                {
                    continue;
                }
                int productId = int.Parse(key.Remove(0, 9)); // remove prefix _quantity to get productId
                form.TryGetValue(key, out var values); // Saves input value to variable values
                int quantity = int.Parse(values.First()); // Gets value from input
                UpdateProductInOrder(order.EOrderId, productId, quantity);
            }
        }

        public void SetPerson(Person person, int? orderId = null)
        {
            var order = GetOrder(orderId, false);
            order.BuyerPersonDetailId = person.PersonDetailId;
            order.BuyerAddressId = person.AddressId;
            order.BuyerDeliveryAddressId = person.DeliveryAddressId;
            order.BuyerId = person.PersonId;
            _eOrderRepository.Update(order);
        }


        public Dictionary<int, string> GetPaymentMethods()
        {
            var paymentCategory = _categoryRepository.GetWayOfPaymentCategory();
            return paymentCategory.CategoryProducts.ToDictionary(cp => cp.ProductId, cp => $"{cp.Product.Title}");
        }

        public Dictionary<int, string> GetTransportMethods()
        {
            var transportCategory = _categoryRepository.GetTransportCategory();
            return transportCategory.CategoryProducts.ToDictionary(cp => cp.ProductId, cp => $"{cp.Product.Title} {cp.Product.Price.ToString()} €");
        }

        public void SetTransportMethod(int transportMethodId)
        {
            var order = GetOrder(null, false);
            order.DeliveryProductId = transportMethodId;
            _eOrderRepository.Update(order);
        }

        public void SetPaymentMethod(int paymentMethodId)
        {
            var order = GetOrder(null, false);
            order.WayOfPaymentId = paymentMethodId;
            _eOrderRepository.Update(order);
        }

        public void CompleteOrder(string emailBody)
        {
            var order = GetOrder(null, false);
            var seller = _personRepository.GetSeller();
            var paymentInAdvance = _productRepository.GetPaymentInAdvance();
            order.SellerId = seller.PersonId;
            order.SellerPersonDetailId = seller.PersonDetailId;
            order.SellerAddressId = seller.AddressId;
            if (order.WayOfPaymentId == paymentInAdvance.ProductId)
            {
                order.OrderState = OrderState.CREATEDAWAITINGPAYMENT;
            }
            else
            {
                order.OrderState = OrderState.CREATED;
            }

            var products = _productEOrderRepository.FindByOrderId(order.EOrderId).ToList();
            var priceOfProducts = products.Sum(p => p.Quantity * p.Product.Price);
            decimal finalPrice = priceOfProducts + order.DeliveryProduct.Price;
            order.FinalPrice = finalPrice;
            _eOrderRepository.Update(order);
            SendOrderCreatedEmail(order, emailBody);
            httpContext.Session.Remove("orderId");
        }


        private void SendOrderCreatedEmail(EOrder order, string emailBody)
        {
            emailSender.SendEmail(order.BuyerPersonDetail.Email, "Objednávka vytvorená", emailBody);
        }

    }
}
