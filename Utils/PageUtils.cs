
using System.Threading.Tasks;
using Microsoft.Playwright;
using PortalTalk.AutomationTest.Pages;

namespace PortalTalk.AutomationTest.Utils
{

    public static class PageUtils
    {
        public static IPage _page;

        public static IPage GetPage()
        {
            return _page;
        }

        public static void SetPage(IPage pageInstance)
        {
            _page = pageInstance;
        }
        // public static async Task<IPage> SetupPageAsync(IPlaywright playwright, IBrowser browser, bool headless = true)
        // {
        //     playwright ??= await Playwright.CreateAsync();
        //     browser ??= await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //     {
        //         // Headless = headless
        //     });
        //     IBrowserContext context = await browser.NewContextAsync();
        //     IPage page = await context.NewPageAsync();
        //     page.SetDefaultTimeout(float.Parse(TimeoutUtils.MAX_TIMEOUT));
        //     context.SetDefaultTimeout(float.Parse(TimeoutUtils.MAX_TIMEOUT));
        //     return page;
        // }
        // public static async Task TeardownAsync(IPlaywright playwright, IBrowser browser)
        // {
        //     if (browser != null)
        //     {
        //         await browser.CloseAsync();
        //     }
        //     playwright?.Dispose();
        // }
    }
}