using E_Shop.Business.Classes;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.OrderViewModels
{
    public class OrderSummaryViewModel
    {
        public OrderSummary OrderSummary { get; set; }
        public IEnumerable<OrderItemInfo> OrderItems { get; set; }
        public OrderItemInfo TransportMethod { get; set; }
        public OrderItemInfo WayOfPayment { get; set; }
        public PersonDetail PersonDetail { get; set; }
        public Address Address { get; set; }
        public Address DeliveryAddress { get; set; }
        public bool Registered { get; set; }
        public int OrderId { get; set; }

    }
}
