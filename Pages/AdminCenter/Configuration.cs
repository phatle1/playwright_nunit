using Microsoft.Playwright;
using PortalTalk.AutomationTest.Utils;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.AdminCenter
{


    public class Configuration(IPage page) : BasePage(page)
    {
        /**Element for MAIN section */
        public readonly string manageWorkSpaceLbl = "//*[contains(@id,'admin-page')]//span[text()='MANAGE WORKSPACES']";

        public readonly string CollapseMenuIco = "//i[@data-icon-name='CollapseMenu']";
        public readonly string configurationIco = "//nav[@role='navigation']//div[@id='configuration']//div[text()='Configuration']//following-sibling::i";
        public readonly string configurationTxt = "//*[@id='configuration']//div[text()='Configuration']";
        public readonly string categoryIco = "//div[@name='General Settings']//parent::li//following-sibling::li/div[@name='Categories']//button";

        [AllureStep("Action: open CATEGORY screen")]
        public async Task ActionOpenCategories()
        {
            await ActionClickConfigIco();
        }

        [AllureStep("Action: Click on CONFIG and Category Icon")]
        public async Task ActionClickConfigIco()
        {
            await Click(CollapseMenuIco);
            await FullWait();
            await Wait(5000);
            bool isDone = true;
            while (isDone)
            {
                await Click(configurationIco);
                bool isDisplayed = await IsElementDisplayed(categoryIco);
                if (isDisplayed)
                {
                    await Click(categoryIco);
                    isDone = false;
                }
                else
                {
                    await Click(configurationIco);
                    await Click(categoryIco);
                    isDone = false;
                }
            }
        }

        [AllureStep("Action: Click on CATEGORY Icon")]
        public async Task ActionClickCategoryIco()
        {
            await Wait(TimeoutUtils.MEDIUM_TIMEOUT);
            await Focus(categoryIco);
            await Click(categoryIco);
        }
    }
}
