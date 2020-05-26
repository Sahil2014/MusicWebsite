using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(15, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        
        public string Genre { get; set; }
        public string CatCoverPic { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public Category()
        {
            Items = new HashSet<Item>();
        }
    }
}