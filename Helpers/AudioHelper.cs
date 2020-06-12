using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace MusicWebsite.Helpers
{
    public class AudioHelper
    {
       public TimeSpan GetAudioDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
               
                IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
        }
    }
}