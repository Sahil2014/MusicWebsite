using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.ViewModels
{
    public class BuyItem
    {
        public int Id { get; set; }
       [Range(1,10, ErrorMessage ="Enter Qty between 1 to 10")]
        
        public int QtyToOrder { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal PriceForAllQty { get; set; }
    }
}