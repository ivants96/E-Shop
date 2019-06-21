using E_Shop.Business.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.OrderViewModels
{
    public class OrderIndexViewModel
    {
        public IEnumerable<OrderItemInfo> OrderItems { get; set; }
        public OrderSummary OrderSummary { get; set; }
    }
}
