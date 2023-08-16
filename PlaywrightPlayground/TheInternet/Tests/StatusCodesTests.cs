using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightPlayground.TheInternet.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightPlayground.TheInternet.Tests;
internal class StatusCodesTests : PlaywrightTest
{
    private IAPIRequestContext request;

    [SetUp]
    public async Task SetUp()
    {
        request = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "https://the-internet.herokuapp.com"
        });
    }

    [TestCase("status_codes/200", 200)]
    [TestCase("status_codes/301", 301)]
    [TestCase("status_codes/404", 404)]
    [TestCase("status_codes/500", 500)]
    public async Task NavigateToPage_ExpectedStatusCodeReturned(string subpage, int expectedStatus)
    {
        var response = await request.GetAsync(subpage);
        Assert.That(response.Status, Is.EqualTo(expectedStatus));
    }
}
