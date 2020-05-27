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
       [Range(1,10)]
        
        public int QtyToOrder { get; set; }
    }
}