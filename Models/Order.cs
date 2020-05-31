using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Buyer's Email")]
        public string Email { get; set; }
        [Required, MaxLength(700)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [MaxLength(15, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        [Display(Name = "Buyer's First Name")]
        public string FirstName { get; set; }
        [MaxLength(15, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        [Display(Name = "Buyer's Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Order Placed On")]
        public DateTime PlacedOn { get; set; }

        public bool IsShipped { get; set; }
        public decimal Total { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal GrandTotal { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }



    }
}