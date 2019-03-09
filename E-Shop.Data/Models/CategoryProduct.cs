using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class CategoryProduct
    {        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
                
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
