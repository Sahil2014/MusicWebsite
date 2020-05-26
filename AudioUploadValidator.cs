using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicWebsite
{
    public class AudioUploadValidator
    {
        public static bool IsWebFriendlyAudio(HttpPostedFileBase file)
        {
            if (file == null)
            { return false; }
            if (file.ContentLength > 100 * 1024 * 1024 || file.ContentLength < 1024)
            { return false; }
            var ext = Path.GetExtension(file.FileName);
            if(ext!=".mp3")
            { return false; }
            return true;
        }
    }
}