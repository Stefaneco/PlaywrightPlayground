using Microsoft.Playwright.NUnit;
using PlaywrightPlayground.TheInternet.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet.Tests;
internal class BrokenImagesTests : PageTest
{
    private BrokenImagesPage page;
    
    [SetUp]
    public void SetUp()
    {
        Page.GotoAsync("https://the-internet.herokuapp.com/broken_images");
        page = new BrokenImagesPage(Page);
    }

    [Test]
    public async Task Test()
    {

    }
}
