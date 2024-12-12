
namespace PortalTalk.AutomationTest.Utils
{
    using Microsoft.Playwright;
    using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
    using NUnit.Framework;
    public class AssertsUtils(IPage page)
    {
        private readonly IPage _page = page;

        public async Task ExpectElementNotToBeAttached(ILocator element)
        {
            await Assertions.Expect(element).Not.ToBeAttachedAsync();
        }
        public async Task ExpectElementToBeVisible(ILocator element)
        {
            await Assertions.Expect(element).ToBeVisibleAsync(new() { Timeout = TimeoutUtils.MEDIUM_TIMEOUT });
        }
        public async Task ExpectElementToHaveText(ILocator element, string expectedText)
        {
            await Assertions.Expect(element).ToHaveTextAsync(expectedText, new LocatorAssertionsToHaveTextOptions { IgnoreCase = true });
        }

        public async Task<bool> IsElementDisplayed(string locator)
        {
            return await page.IsVisibleAsync(locator);
        }

        public bool IsEqual(object Actual, object Expected)
        {
            try
            {
                bool result = Equals(Actual, Expected);
                return result;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
