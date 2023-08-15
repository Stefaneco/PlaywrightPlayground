using Microsoft.Playwright.NUnit;
using PlaywrightPlayground.TheInternet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.Base;
internal class BaseTest : PageTest
{
    [TearDown]
    public async Task TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var testName = TestContext.CurrentContext.Test.MethodName ?? "MISSING_TEST_NAME";
            var screenshotName = testName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
            await Page.ScreenshotAsync(new()
            {
                Path = Path.Combine(Globals.GetScreenshotFolderPath(), screenshotName),
                FullPage = true
            });
        }
    }
}
