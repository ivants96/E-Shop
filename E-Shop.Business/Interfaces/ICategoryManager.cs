using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface ICategoryManager
    {
        List<Category> GetLeaves(bool includeHidden = false);
        List<Category> GetRoots();
        void UpdateProductCategories(int productId, int[] categories);
        Category GetPaymentMethodsCategory();
        Category GetTransportMethodsCategory();



    }
}
