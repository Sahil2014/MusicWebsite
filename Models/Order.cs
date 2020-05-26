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
        public string Email { get; set; }
        [Required, MaxLength(700)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [MaxLength(15, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        public string FirstName { get; set; }
        [MaxLength(15, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        public string LastName { get; set; }

        public bool IsShipped { get; set; }
        public decimal Total { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal GrandTotal { get; set; }
        
        

    }
}