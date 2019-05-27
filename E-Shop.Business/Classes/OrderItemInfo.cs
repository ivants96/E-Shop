using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Classes
{
    public class OrderItemInfo
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
    }
}
