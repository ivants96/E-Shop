using E_Shop.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
    public class PassCartStateFilterAttribute : ResultFilterAttribute
    {
        // Shows total price of items in the cart
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var provider = context.HttpContext.RequestServices;
            var orderManager = GetService<IOrderManager>(provider);
            ((Controller)context.Controller).ViewData["CartItemsSum"] = orderManager.GetOrderSummary().Price;
        }

        private T GetService<T>(IServiceProvider services)
        {
            return (T)services.GetService(typeof(T));
        }


    }
}
