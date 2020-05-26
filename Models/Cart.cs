using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class Cart
    {
        
        public int Id { get; set; }
        public string CartNumber { get; set; }

        public const string CartSessionKey = "CartNumber";

       

        
        public int ItemsInCart { get; set; }
        public decimal Total { get; set; }


    }
}