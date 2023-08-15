using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet;
internal static class Globals
{
    public static string GetScreenshotFolderPath()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var parentPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        return Path.Combine(parentPath, "Screenshots" );
    }
}
