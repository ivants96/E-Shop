using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class EOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int EOrderId { get; set; }
        public string Token { get; set; }
        public int? Number { get; set; }
        public DateTime Created { get; set; }
        public DateTime Issued { get; set; }
        public DateTime DueDate { get; set; }
        public OrderState OrderState { get; set; }

        public virtual Person Buyer { get; set; }
        public virtual Person Seller { get; set; }
        public virtual PersonDetail BuyerPersonDetail { get; set; }
        public virtual PersonDetail SellerPersonDetail { get; set; }
        public virtual Address BuyerAddress { get; set; }
        public virtual Address BuyerDeliveryAddress { get; set; }
        public virtual Address SellerAddress { get; set; }        

        [ForeignKey("Buyer")]
        public int? BuyerId { get; set; }
        [ForeignKey("Seller")]
        public int? SellerId { get; set; }
        [ForeignKey("BuyerPersonDetail")]
        public int? BuyerPersonDetailId { get; set; }
        [ForeignKey("SellerPersonDetail")]
        public int? SellerPersonDetailId { get; set; }
        [ForeignKey("BuyerAddress")]
        public int? BuyerAddressId { get; set; }
        [ForeignKey("BuyerDeliveryAddress")]
        public int? BuyerDeliveryAddressId { get; set; }
        [ForeignKey("SellerAddress")]
        public int? SellerAddressId { get; set; }
        [ForeignKey("DeliveryProduct")]
        public int? DeliveryProductId { get; set; }

        public virtual Product  DeliveryProduct { get; set; }
        public virtual ICollection<ProductEOrder> ProductEOrders { get; set; }
        public decimal ?FinalPrice { get; set; }

        public EOrder()
        {
            this.ProductEOrders = new List<ProductEOrder>();
        }

    }

    public class ProductEOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int ProductEOrderId { get; set; }
        public int ProductId { get; set; }
        public int EOrderId { get; set; }
        public int Quantity { get; set; }
        public virtual EOrder EOrder { get; set; }
        public virtual Product Product { get; set; }
    }

    public enum OrderState : byte
    {
        [Description("Zrušená")]
        CANCELED,
        [Description("Vytvorená")]
        CREATED,
        [Description("Pozastavená")]
        SUSPENDED,
        [Description("Potvrdená")]
        ACCEPTED,
        [Description("Dokončená")]
        COMPLETED,
        [Description("Odoslaná")]
        SENT
    }
}
