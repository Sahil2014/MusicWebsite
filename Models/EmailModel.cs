using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Email"), EmailAddress]
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}