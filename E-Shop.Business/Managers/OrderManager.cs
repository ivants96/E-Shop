using E_Shop.Business.Classes;
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
        private IEOrderRepository _eOrderRepository;
        private ICategoryRepository _categoryRepository;
        private readonly HttpContext httpContext;

        public OrderManager
            (
            IProductRepository productRepository,
            IProductEOrderRepository productEOrderRepository,
            IEOrderRepository eOrderRepository,
            ICategoryRepository categoryRepository,
            IHttpContextAccessor context
            )
        {
            _productRepository = productRepository;
            _productEOrderRepository = productEOrderRepository;
            _eOrderRepository = eOrderRepository;
            _categoryRepository = categoryRepository;
            httpContext = context.HttpContext;
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

        public EOrder GetOrder(int? orderId, bool create = true)
        {
            if (orderId.HasValue)
            {
                _eOrderRepository.FindById(orderId.Value);
            }


            int? attemptRetrieve = httpContext.Session.GetInt32("orderId");
            if (attemptRetrieve.HasValue)
            {
                _eOrderRepository.FindById(attemptRetrieve.Value);
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
                CreateOrder();
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

        public void AddProducts(int productId, int quantity, int? orderId, bool ignoreHiddenProducts = false)
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
                // Ak je produkt už v košíku zmeníme iba jeho miesto toho aby sme ho pridali znova
                item.Quantity += quantity;
                _productEOrderRepository.Update(item);
            }
        }

        public OrderSummary GetOrderSummary(int? orderId = null)
        {
            var order = GetOrder(orderId, false);

            if (order == null)
            {
                return new OrderSummary() { Price = 0m, Quantity = 0 };
            }
            var items = _productEOrderRepository.FindByOrderId(order.EOrderId);
            var result = new OrderSummary()
            {
                Price = items.Sum(i => i.Quantity * i.Product.Price),
                Quantity = items.Sum(i => i.Quantity)
            };
            return result;
        }


    }
}
