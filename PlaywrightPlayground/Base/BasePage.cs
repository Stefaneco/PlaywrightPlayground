using Microsoft.Playwright;
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
}
