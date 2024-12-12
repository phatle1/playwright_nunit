
using Microsoft.Playwright;
using PortalTalk.AutomationTest.Utils;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.UserApplication
{
    public class CreateNewWSPopups(IPage page) : BasePage(page)
    {
        public string submittedTime = "";

        #region Confirmation section
        private readonly string parentPopup = "//div[@id='create-workspace-container']";
        private readonly string currentDateTimeLbl = "//*[@id='workspace-content']//span[contains(text(),'Dashboard')]//following-sibling::span";
        private readonly string noThanksBtn = "//div[@id='create-workspace-container']//button[1]";
        private readonly string yesIwantToCreateBtn = "//div[@id='create-workspace-container']//button[2]";
        #endregion


        #region Select Category section
        private readonly string selectCategoryDdl = "//span[contains(@id,'Dropdown') and (contains(text(),'Select the category'))]";
        private readonly string dropdownItemsInner = "div[class*='ms-Callout-main'][class*='calloutMain']";
        private static string CategoryiItem(string val) => $"//div[contains(@class,'dropdownItemsWrapper')]//span[text()='{val}']";
        private readonly string nextBtn = "//div[contains(@id,'ModalFocusTrap')]//span[contains(text(),'Next')]";
        private readonly string WSNameInp = "//input[contains(@id,'TextField') and contains(@placeholder,'Tip:')]";
        private readonly string descriptionAreaText = "//textarea[contains(@id,'TextField')]";
        private readonly string OwnersInp = "//input[contains(@id,'combobox-id') and contains(@placeholder,'Find and select a')]";
        private string SuggestedUsesSelection(string val) => $"//div[contains(@class,'ms-Suggestions-container') and contains(@role,'presentation')]//div[contains(text(),'{val}')]";
        #endregion



        #region Create Workspace section
        private readonly string CreateNewWSHeader = "//div[text()='Create New Workspace']";
        // private readonly string WSName = "//input[contains(@placeholder,'Tip: Choose a suitable and recognizable name for the workspace') and contains(@id,'TextField')]";
        // private readonly string WSDescription = "//textarea";
        // private readonly string OwnersInp = "//input[contains(@placeholder,'Find and select a user...')]";

        #endregion

        // Update Workspace logo section
        private readonly string FinishBtn = "//span[contains(text(),'Finish')]";

        // Creating new workspace section
        private readonly string SubmitBtn = "//span[contains(text(),'OK, I understand')]";


        [AllureStep("Action: Click on Yes, I want to create a new one")]
        public async Task ActionClickOnYesIwantToCreateBtn()
        {
            await Click(yesIwantToCreateBtn);
        }

        [AllureStep("Action: Click on 'Ok, I understand' button")]
        public async Task ActionClickOnOKIUnderstandBtn()
        {
            await Click(SubmitBtn);
        }

        [AllureStep("Action: Click on 'Finish' button")]
        public async Task ActionClickOnFinishBtn()
        {
            await Click(FinishBtn);
        }

        [AllureStep("Assert: Create new WS popup is displayed")]
        public async Task AssertConfirmToCreateNewWSIsDisplayed()
        {
            await ExpectElementToBeVisible(noThanksBtn);
            await ExpectElementToBeVisible(yesIwantToCreateBtn);
        }

        [AllureStep("Assert: Select Category is displayed")]
        public async Task AssertSelectCategoryIsDisplayed()
        {
            await ExpectElementToBeVisible(selectCategoryDdl);
        }

        [AllureStep("Action: Create new workspace")]
        public async Task ActionCreateNewWS(string catName)
        {
            await ActionSelectNewWSCategory(catName);
            await ActionCreateNewWS_InputNewWSNameAndOwners(catName);
            await ActionClickOnFinishBtn();
            await ActionClickOnOKIUnderstandBtn();
            submittedTime = DateTimeUtils.GetCurrentDateTime("M/d/yyyy, h:mm tt");
            await Wait(TimeoutUtils.STANDARD_TIMEOUT);
        }

        [AllureStep("Action: Select New Workspace Category")]
        public async Task ActionSelectNewWSCategory(string catName)
        {
            await Click(selectCategoryDdl);
            await ScrollDownByKeyboardUntilElement(CategoryiItem(catName), dropdownItemsInner);
            await Click(CategoryiItem(catName));
            await Click(nextBtn);
        }

        [AllureStep("Action: Create New Workspace - Input New Workspace Name and Owners")]
        public async Task ActionCreateNewWS_InputNewWSNameAndOwners(string catName)
        {
            await Fill(WSNameInp, catName);
            await Click(descriptionAreaText);
            await Fill(descriptionAreaText, "Description: " + catName);
            await Fill(OwnersInp, EnvUtils.USERNAME);
            await Click(OwnersInp);
            await Click(SuggestedUsesSelection(EnvUtils.USERNAME));
            await Click(nextBtn);
        }


    }
}
