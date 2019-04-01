using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.ProductViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public int Vat { get; set; }
        public bool IsVatPayer { get; set; }
    }
}
