using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int itemId { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a quantity between 0 and 50")]
        public int QtyToOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Item Item { get; set; }
    }
}