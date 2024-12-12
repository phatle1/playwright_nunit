using Microsoft.Playwright;
using NUnit.Framework;
using PortalTalk.AutomationTest.Utils;
using System.Threading.Tasks;

namespace PortalTalk.AutomationTest.Pages
{
    public class BasePage(IPage page)
    {
        // protected readonly IPage Page = page;
        protected readonly ActionUtils Actions = new(page);
        protected readonly AssertsUtils Asserts = new(page);

        // Common navigation methods
        protected async Task NavigateToAsync(string url)
        {
            await Actions.GoTo(url);
            await Actions.WaitForLoadStateAsync();
        }


        // Common element interaction methods

        public async Task<ILocator> GetElement(string locator)
        {
            await FullWait();
            var element = Actions.GetElement(locator);
            return element;
        }

        public async Task<IEnumerable<IElementHandle>> GetElements(string locator)
        {
            await FullWait();
            var elements = await Actions.GetElements(locator);
            return elements;
        }

        public async Task<string> GetInnerTextAsync(string locator)
        {
            await FullWait();
            var element = await Actions.GetInnerTextAsync(locator);
            return element;
        }

        public async Task<int> GetCountAsync(string selector)
        {
            var element = await GetElement(selector);
            var element_count = await element.CountAsync();
            return element_count;
        }

        protected async Task GoTo(string url)
        {
            await Actions.GoTo(url);
        }
        protected async Task Fill(string selector, string value)
        {
            await Actions.FillAsync(selector, value);
        }

        protected async Task FillElements(string selector, string value)
        {
            await Actions.FillAsync(selector, value);
        }

        protected async Task Click(string selector)
        {
            await Actions.ClickAsync(selector);
        }

        protected async Task Focus(string selector)
        {
            await Actions.FocusAsync(selector);
        }

        protected async Task Hover(string selector)
        {
            await Actions.ClickAsync(selector);
        }

        protected async Task FullWait()
        {
            await Actions.FullWait();
        }

        protected async Task Wait(float times)
        {
            await Actions.Wait(times);
        }

        protected async Task Reload(int times)
        {
            await Actions.Reload(times);
        }

        protected async Task ReloadUntilElementFound(string locator, int maxAttempts = 10)
        {
            await Actions.ReloadUntilElementFound(locator, maxAttempts);
        }
        protected async Task<string> GetTextAsync(string selector)
        {
            return await Actions.GetTextAsync(selector);
        }

        protected async Task<bool> IsElementVisibleAsync(string selector)
        {
            return await Actions.IsVisibleAsync(selector);
        }


        public async Task ExpectElementNotToBeAttached(string selector)
        {
            await Actions.ExpectElementNotToBeAttached(selector);
        }

        public async Task ExpectElementToHaveText(string selector, string val)
        {
            await Actions.ExpectElementToHaveText(selector, val);
        }

        protected async Task SelectDropdownOptionAsync(string selector, string value)
        {
            await Actions.SelectOptionAsync(selector, value);
        }

        protected async Task<bool> ScrollDownByKeyboardUntilElement(string selectorToFind, string scrollableElementInsideCategoryTable)
        {
            var atBottom = await Actions.ScrollDownByKeyboardUntilElement(selectorToFind, scrollableElementInsideCategoryTable);
            return atBottom;
        }

        protected async Task<bool> ScrollDownToBottom(string scrollableElement)
        {
            var atBottom = await Actions.ScrollDownToBottom(scrollableElement);
            return atBottom;
        }




        // Common verification methods
        protected async Task AssertElementTextAsync(string selector, string expectedText)
        {
            var actualText = await GetTextAsync(selector);
            Assert.That(actualText, Is.EqualTo(expectedText));
        }

        protected async Task ExpectElementToBeVisible(string selector)
        {
            await Actions.ExpectElementToBeVisible(selector);
        }


        public async Task<bool> IsElementDisplayed(string locator)
        {
            await FullWait();
            bool isDisplayed = await Actions.IsVisibleAsync(locator);
            return isDisplayed;
        }

        public static string CleanString(string input)
        {
            if (input != null)
            {
                return input.Replace("\\\"", "\"").Trim();
            }
            return "";
        }

        public static bool IsEqual(string Actual, string Expected)
        {
            var temp_actual = CleanString(Actual);
            var temp_expected = CleanString(Expected);
            if (temp_actual == temp_expected)
            { return true; }
            else
            { return false; }
        }
        protected static void UpdateAccessToken(string env, string token)
        {
            FileUtils.UpdateAccessTokenFromENV(env, token);
        }

    }
}