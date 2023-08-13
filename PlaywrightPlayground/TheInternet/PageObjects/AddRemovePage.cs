using Microsoft.Playwright;
using PlaywrightPlayground.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet.PageObjects;
internal class AddRemovePage : BasePage
{
    private ILocator AddButton =>
        page.GetByRole(AriaRole.Button, new() { Name = "Add Element" });

    private ILocator DeleteButton(int index) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).Nth(index);

    private ILocator DeleteButtons =>
        page.GetByRole(AriaRole.Button, new() { Name = "Delete" });

    public AddRemovePage(IPage page) : base(page){}

    public async Task ClickAddButton()
    {
        await AddButton.ClickAsync();
    }

    public async Task ClickDeleteButton(int index)
    {
        await DeleteButton(index).ClickAsync();
    }

    public ILocator GetDeleteItems()
    {
        return DeleteButtons;
    }
}
