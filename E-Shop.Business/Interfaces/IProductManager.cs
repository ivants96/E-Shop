using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IProductManager
    {
        Product FindProductById(int id);
        Product FindProductByUrl(string url);
    }
}
