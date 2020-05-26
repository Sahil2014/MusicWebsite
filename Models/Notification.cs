﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicWebsite.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }

    }
}