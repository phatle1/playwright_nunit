using Azure;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PortalTalk.AutomationTest.Utils
{
    public class ActionUtils(IPage page)
    {
        private readonly IPage _page = page;

        public ILocator GetElement(string selector)
        {
            return _page.Locator(selector);
        }

        public async Task<List<IElementHandle>> GetElements(string selector)
        {
            var elements = await _page.QuerySelectorAllAsync(selector);
            var locators = new List<IElementHandle>();
            foreach (var element in elements)
            {
                locators.Add(element);
            }
            return locators;
        }

        public async Task<string> GetInnerTextAsync(string selector)
        {
            var text = await GetElement(selector).InnerTextAsync();
            return text ?? "";
        }

        public async Task GoTo(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task FillAsync(string selector, string value)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await element.FillAsync(value);
        }

        public async Task ClickAsync(string selector)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await element.ClickAsync();
        }

        public async Task FocusAsync(string selector)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await element.FocusAsync();
        }

        public async Task HoverAsync(string selector)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await element.HoverAsync();
        }

        public async Task Reload(int MaxReload)
        {
            int i = 0;
            while (i < MaxReload)
            {
                await _page.ReloadAsync();
                i++;
            }
        }

        public async Task FullWait()
        {
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await _page.WaitForLoadStateAsync(LoadState.Load, new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
        }

        public async Task Wait(float TimeOut)
        {
            await _page.WaitForTimeoutAsync(TimeOut);
        }
        public async Task<bool> ReloadUntilElementFound(string locator, int maxAttempts = 10)
        {

            var attempts = 0;
            while (attempts < maxAttempts)
            {
                try
                {
                    await page.WaitForSelectorAsync(locator, new()
                    {
                        Timeout = TimeoutUtils.BIG_TIMEOUT,
                        State = WaitForSelectorState.Attached
                    });
                    return true;
                }
                catch (TimeoutException)
                {
                    attempts++;
                    if (attempts >= maxAttempts)
                        return false;

                    await Wait(TimeoutUtils.INSTANT_TIMEOUT);
                    await page.ReloadAsync();
                }
            }
            return false;
        }

        public async Task<bool> ScrollDownToBottom(string scrollableElement)
        {
            try
            {
                /*Note: The element should be in CSS form*/
                await FullWait();
                bool scrolling = true;
                var scrollableElementHeight = await _page.QuerySelectorAsync(scrollableElement);
                var atBottom = await _page.EvaluateAsync<bool>(@"(scrollableElement) => {
                    const element = document.querySelector(scrollableElement);
                    if (!element) return false;
                    return element.scrollHeight - element.scrollTop === element.clientHeight;
                }", scrollableElement);
                if (!atBottom)
                {
                    scrolling = await _page.EvaluateAsync<bool>(@"scrollableElement => {
                        const element = document.querySelector(scrollableElement);
                            if (!element) return false;
                            const initialScrollTop = element.scrollTop;
                            checkheight = element.clientHeight;
                            element.scrollTop += element.clientHeight;
                            return element.scrollTop > initialScrollTop;
                    }", scrollableElement);
                    await _page.WaitForTimeoutAsync(TimeoutUtils.MIN_TIMEOUT);
                }
                return atBottom;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> ScrollDownByKeyboardUntilElement(string selectorToFind, string scrollableElementInsideCategoryTable)
        {
            await FullWait();
            bool isScroll = true;
            bool isElementDisplayed = false;
            while (isScroll)
            {
                await _page.Locator(scrollableElementInsideCategoryTable).FocusAsync();
                await ScrollDownToBottom(scrollableElementInsideCategoryTable);
                var displayed = await IsElementDisplayed(selectorToFind);
                isElementDisplayed = displayed;
                if (isElementDisplayed == true)
                {
                    isScroll = false;
                }
            }
            return isElementDisplayed;
        }

        public async Task<string> GetTextAsync(string selector)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            var text = await element.TextContentAsync();
            return text ?? "";
        }

        public async Task<bool> IsVisibleAsync(string selector)
        {
            try
            {
                var element = GetElement(selector);
                await element.WaitForAsync(new() { Timeout = TimeoutUtils.STANDARD_TIMEOUT });
                return await element.IsVisibleAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task WaitForLoadStateAsync()
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task SelectOptionAsync(string selector, string value)
        {
            var element = GetElement(selector);
            await element.WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
            await element.SelectOptionAsync(value);
        }

        public async Task WaitForElementAsync(string selector)
        {
            await _page.Locator(selector).WaitForAsync(new() { Timeout = TimeoutUtils.MAX_TIMEOUT });
        }

        public async Task ExpectElementToBeVisible(string selector)
        {
            var element = GetElement(selector);
            Assert.That(selector, Is.Not.Null, $"Element with selector '{selector}' not found.");
            await Assertions.Expect(element).ToBeVisibleAsync(new() { Timeout = 60000 });
        }

        public async Task ExpectElementToHaveText(string selector, string expectedText)
        {
            var element = GetElement(selector);
            Assert.That(selector, Is.Not.Null, $"Element with selector '{selector}' not found.");
            await Assertions.Expect(element).ToHaveTextAsync(expectedText, new LocatorAssertionsToHaveTextOptions { IgnoreCase = true, Timeout = TimeoutUtils.STANDARD_TIMEOUT });
        }

        public async Task ExpectElementNotToBeAttached(string selector)
        {
            var el = GetElement(selector);
            Assert.That(selector, Is.Not.Null, $"Element with selector '{selector}' not found.");
            await Assertions.Expect(el).Not.ToBeAttachedAsync();
        }

        public async Task<bool> IsElementDisplayed(string locator)
        {
            await FullWait();
            return await _page.IsVisibleAsync(locator);
        }

        public static bool IsEqual(object Actual, object Expected)
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