using E_Shop.Business.Classes;
using E_Shop.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IOrderManager
    {
        EOrder CreateOrder();
        EOrder GetOrder(int? orderId = null, bool create = true);
        bool IsProductAvailable(int productId);
        void AddProducts(int productId, int quantity, int? orderId = null, bool ignoreHiddenProducts = false);
        OrderSummary GetOrderSummary(int? orderId = null);
        List<OrderItemInfo> GetProducts(int? orderId = null);
        void UpdateProductInOrder(int orderId, int productId, int quantity);
        void UpdateCart(IFormCollection form);
        void SetPerson(Person person, int? orderId = null);
        Dictionary<int, string> GetTransportMethods();
        Dictionary<int, string> GetPaymentMethods();
        void SetTransportMethod(int transportMethodId);
        void SetPaymentMethod(int paymentMethodId);
        void CompleteOrder(string emailBody);
        
    }
}
