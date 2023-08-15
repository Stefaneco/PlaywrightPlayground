using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightPlayground.TheInternet.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet.Tests;
internal class AddRemoveTests : PageTest
{
    private AddRemovePage page;

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
        page = new AddRemovePage(Page);
    }

    [TestCase(4,3)]
    public async Task AddAndDeleteItems_ItemsAreAddedAndDeleted(int amountOfItemsAdded, int indexOfItemToBeDeleted)
    {
        for(int i = 0; i < amountOfItemsAdded; i++)
        {
            await page.ClickAddButton();
        }
        await page.ClickDeleteButton(indexOfItemToBeDeleted);
        await Expect(page.GetDeleteItems()).ToHaveCountAsync(amountOfItemsAdded - 1);
    }
}
