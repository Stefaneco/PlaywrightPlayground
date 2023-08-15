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
        page.GetByRole(AriaRole.Img);

    private ILocator image(int index) =>
        page.GetByRole(AriaRole.Img).Nth(index);
    public BrokenImagesPage(IPage page) : base(page) { }

    public ILocator GetImages() => images;

    public ILocator GetImage(int index)
    {
        return image(index);
    }

    public async Task<string> GetImageProperty(int index, string propertyName)
    {
        var propertyValue = await image(index).EvaluateAsync<string>($"element => element.{propertyName}")
            ?? throw new Exception("Image or Attribute not found");
        return propertyValue;
    }

    public async Task<int> GetImagesCount()
    {
        return await images.CountAsync();
    }
}
