using Microsoft.Playwright;
using PlaywrightPlayground.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet.PageObjects;
internal class BrokenImagesPage : BasePage
{
    private ILocator images =>
        page.Locator("img");
    public BrokenImagesPage(IPage page) : base(page) { }

    public ILocator GetImages() => images;
}
