using Microsoft.Playwright;
using PlaywrightPlayground.TheInternet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.Base;
internal class BasePage
{
    protected readonly IPage page;

    public BasePage(IPage page)
    {
         this.page = page;
    }

    public async Task Screenshot(string testName)
    {
        var screenshotName = testName + "_" + DateTime.Now.ToString();
        await page.ScreenshotAsync(new()
        {
            Path = Path.Combine(Globals.GetScreenshotFolderPath(), screenshotName),
            FullPage = true
        });
    }
}
