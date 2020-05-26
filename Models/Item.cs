using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class Item
        
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        
        public int? CategoryId { get; set; }
       
        public DateTime AddedOn { get; set; }
        public int? FileSize { get; set; }

        public string FilePath { get; set; }

        public string CoverPic { get; set; }
        [Required, Range(0,1000, ErrorMessage = "Price must be between 0 and 1000")]
        public decimal Price { get; set; }
        [Required, Range(0, 1000, ErrorMessage = "Qty must be between 0 and 1000")]
        public int Qty { get; set; }
      

        public virtual Category category { get; set; }

       
        

    }
}