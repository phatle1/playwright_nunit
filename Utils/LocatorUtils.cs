
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PortalTalk.AutomationTest.Utils
{
    public class LocatorUtils(IPage pageInstance)
    {
        private IPage page = pageInstance;

        public ILocator GetElement(string locator)
        {
            return page.Locator(locator);
        }
        public ILocator GetShadowElement(string locator)
        {
            return page.Locator(locator);
        }
    }
}