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
    public async Task SetUp()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/broken_images");
        page = new BrokenImagesPage(Page);
    }

    [Test]
    public async Task OpenPage_AllImagesLoaded()
    {
        var amountOfBrokenImages = 0;
        var imagesCount = await page.GetImagesCount();
        Assert.That(imagesCount, Is.EqualTo(4));
        var errorMessage = "Broken images:";
        for (int i = 0; i < imagesCount; i++)
        {
            var imageNaturalWidth = await page.GetImageProperty(i, "naturalWidth");
            if (imageNaturalWidth == "0")
            {
                var imageOuterHtml = await page.GetImageProperty(i, "outerHTML");
                errorMessage += $" {imageOuterHtml}";
                amountOfBrokenImages++;
            }
        }
        Assert.That(amountOfBrokenImages, Is.EqualTo(0), errorMessage);
    }
}
