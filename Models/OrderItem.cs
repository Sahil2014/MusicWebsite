using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int itemId { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a quantity between 0 and 50")]
        public int QtyToOrder { get; set; }
        public DateTime DateCreated { get; set; }
        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }

        public virtual Item Item { get; set; }

        public virtual Order Order { get; set; }
    }
}