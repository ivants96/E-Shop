using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.OrderViewModels
{
    public class OrderPaymentViewModel
    {
        [Required]
        public SelectList TransportationMethods { get; set; }
        [Required]
        public SelectList WaysOfPayment { get; set; }
    }
}
