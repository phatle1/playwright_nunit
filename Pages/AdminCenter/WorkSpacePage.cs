using Microsoft.Playwright;
using PortalTalk.AutomationTest.Utils;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.AdminCenter
{

    public class WorkSpacePage(IPage page) : BasePage(page)
    {
        #region Element for MAIN section
        private readonly string mangeWorkSpaceLbl = "//*[contains(@id,'admin-page')]//span[text()='MANAGE WORKSPACES']";
        private readonly string configurationIco = "//*[@id='configuration']/div[contains(@class,'icon')]";
        private readonly string categoryIco = "//*[@id='configuration']//parent::div//button[@title='Categories']";
        #endregion


        #region Elements for WORKSPACE CATEGORY CONFIGURATION
        private readonly string addNewCategoryBtn = "//*[contains(text(),'Add New Category')]";
        private readonly string generalSettingIco = "//button[@title='General Settings']";
        private readonly string standardScreenLbl = "//*[contains(text(),'Standard Screen')]";
        #endregion

        #region Elements for CATEGORY INFORMATION POPUP
        private readonly string categoryNameInp = "//*[@placeholder='Please enter name of category']";
        private readonly string displayOrderInp = "//*[text()='Display Order']//following-sibling::span//input";
        private readonly string selectCategoryTypeDDL = "//*[text()='Select category type']/following-sibling::div//button[1]";
        #endregion

        #region Elements for CATEGORY SETTING POPUP
        private readonly string catSettingHeaderLbl = "//span[contains(text(),'ADD NEW CATEGORY')]";
        private readonly string prefixGeneratorDDL = "//span[contains(text(),'Prefix generator')]";
        private readonly string prefixGeneratorInp = "//input[contains(@id,'TextField')]";
        private readonly string exampleNameTxt = "(//input[contains(@id,'TextField')]//ancestor::div[contains(@class,'pt-text-field-container')]//following::div[3])[1]";
        #endregion


        #region Elements for Manage Workspace Types SECTION
        private readonly string addNewWorkSpaceTypeBtn = "//button[contains(text(),'Add New Workspace Type')]";
        private readonly string workspaceTypeNameInp = "//input[contains(@placeholder,'Please enter workspace type name')]";
        private readonly string groupAndUserPermissionInp = "//input[contains(@id,'combo')]";
        private readonly string saveNewWorkspaceTypeBtn = "//button[contains(text(),'Save New W')]";

        #endregion



        #region Elements for MANAGE METADATA SECTION
        private readonly string addNewMetadataBtn = "//button[contains(text(),'Add New Metadata')]";
        private readonly string nextStepBtn = "//button[text()='Next Step']";
        private readonly string finishBtn = "//button[contains(text(),'Finish')]";
        private readonly string loadingTxt = "//div/span[contains(text(),'LOADING')]";
        private readonly string catCreatedSuccessfullyTxt = ("//span[contains(text(),'The Category created successfully')]");

        #endregion

        #region Elements in table
        private readonly string firstItemOfTable = ("//div[@role='presentation' and @class='ms-DetailsList-contentWrapper']//div[@data-list-index='0']");
        private readonly string scrollableElementInsideCategoryTable = "div[data-is-scrollable='true']";
        private readonly string catNameOnRemoveConfirmationPopup = ("//div[contains(text(),'Remove Category')]//following-sibling::div//span");
        private readonly string yesRemoveCatNameOnRemoveConfirmationPopup = ("//button[contains(text(),'Yes, Remove')]");
        #endregion

        #region dynamic Elements
        public static string CategoryType(string val) => $"//*[contains(@id,'fluent-option') and contains(text(),'{val}')]";
        public static string GetAddedCategoryFromTable(string catName) => $"//div[contains(@class,'ScrollablePane')]//div[@role='gridcell'][3]//span[contains(text(),'{catName}')]";
        public static string GetEditDeleteButtonBasedOnCatName(string catName) => $"//span[contains(text(),'{catName}')]//ancestor::div[@data-automationid='ListCell']//button";

        #endregion


        #region Elements from Main table and delete popup
        private readonly string EditBtn = "//span[contains(text(),'Edit')]";
        private readonly string RemoveBtn = "//span[contains(text(),'Remove')]";
        private readonly string FirstItemOfTable = "//div[@role='presentation' and @class='ms-DetailsList-contentWrapper']//div[@data-list-index='0']";
        private readonly string ScrollableElementInsideCategoryTable = "div[data-is-scrollable='true']";
        private readonly string CatNameOnRemoveConfirmationPopup = "//div[contains(text(),'Remove Category')]//following-sibling::div//span";
        private readonly string YesRemoveCatNameOnRemoveConfirmationPopup = "//button[contains(text(),'Yes, Remove')]";
        #endregion



        [AllureStep("Action: Fill SELECT CATEGORY form")]
        public async Task ActionAddNewCategory(string catName, string catOrd, string catType, string prefix, string userName)
        {
            await ActionFillAddNewCatForm(catName, catOrd, catType);
            await ActionFillCatSettingsForm(catName, prefix);
            await ActionFillManageWorkspaceTypeForm(catName, userName);
            await ActionFillManageMetadataForm();
            await ClickOnFinishBtn();
            await AssertLoadingPopupIsDisplayed();
            await AssertAddNewCatTxtIsDisplayed();
            await AssertNewCategoryIsAdded(catName);
        }
        [AllureStep("Assert: The new Category should be added to the table")]
        private async Task AssertNewCategoryIsAdded(string catName)
        {
            await Click(firstItemOfTable);
            await ScrollDownByKeyboardUntilElement(GetAddedCategoryFromTable(catName), ScrollableElementInsideCategoryTable);
            await ExpectElementToBeVisible(GetAddedCategoryFromTable(catName));
        }

        [AllureStep("Assert: The new Category should be added to the table")]
        private async Task AssertAddNewCatTxtIsDisplayed()
        {
            await ExpectElementToBeVisible(catCreatedSuccessfullyTxt);
        }
        [AllureStep("Assert: LOADING popup should be displayed")]
        private async Task AssertLoadingPopupIsDisplayed()
        {
            await ExpectElementToBeVisible(loadingTxt);
        }
        [AllureStep("Action: Click on FINISH button")]
        private async Task ClickOnFinishBtn()
        {
            await Click(finishBtn);
        }
        [AllureStep("Action: Fill 'MANAGE METADATA' form")]
        private async Task ActionFillManageMetadataForm()
        {
            await AssertAddNewMetadataBtn();
        }
        [AllureStep("Assert: ADD NEW METADATA button should be displayed")]
        private async Task AssertAddNewMetadataBtn()
        {
            await ExpectElementToBeVisible(addNewMetadataBtn);
        }
        [AllureStep("Action: Fill 'MANAGE WORKSPACE TYPES' form")]
        private async Task ActionFillManageWorkspaceTypeForm(string WorkspaceTypeName, string userName)
        {
            await AssertManageWorkSpaceTypeIsDisplayed();
            await ClickAddNewWorkspaceTypeBtn();
            await FillCatTypeName(WorkspaceTypeName);
            await FillUserGroupPermission(userName);
            await ClickSaveNewWorkspaceTypeBtn();
            await ActionClickNextBtn();
        }
        [AllureStep("Assert: Prefix MANAGE WORK SPACE should be displayed")]
        private async Task AssertManageWorkSpaceTypeIsDisplayed()
        {
            await ExpectElementToBeVisible(addNewWorkSpaceTypeBtn);
        }
        [AllureStep("Action: Click on Add New Workspace Type button")]
        private async Task ClickAddNewWorkspaceTypeBtn()
        {
            await Click(addNewWorkSpaceTypeBtn);
        }
        [AllureStep("Action: Click on Save New Workspace Type button")]
        private async Task ClickSaveNewWorkspaceTypeBtn()
        {
            await Click(saveNewWorkspaceTypeBtn);
        }
        [AllureStep("Action: Fill to Category Type Name")]
        private async Task FillCatTypeName(string catName)
        {
            await Fill(workspaceTypeNameInp, catName);
        }
        [AllureStep("Action: Fill User Group Permission")]
        private async Task FillUserGroupPermission(string user)
        {
            await Fill(groupAndUserPermissionInp, user);
        }
        [AllureStep("Action: Fill 'CATEGORY SETTINGS' form")]
        private async Task ActionFillCatSettingsForm(string catName, string prefix)
        {
            await AssertCategoryHeaderIsDisplayed($"ADD NEW CATEGORY: ${catName}");
            await AssertPrefixGeneratorDDLIsDisplayed();
            // await ActionClickOnPrefixDDL();
            await ActionFillOnPrefixInp(prefix);
            await AssertPrefixExampleNameIsDisplayed(prefix);
            await ActionClickNextBtn();
        }
        [AllureStep("Assert: WORKSPACE PREFIX EXAMPLE should be displayed")]
        private async Task AssertPrefixExampleNameIsDisplayed(string prefix)
        {
            await ExpectElementToHaveText(exampleNameTxt, prefix);
        }
        [AllureStep("Action: Fill PREFIX inp")]
        private async Task ActionFillOnPrefixInp(string prefix)
        {
            await Click(prefixGeneratorDDL);
            await Fill(prefixGeneratorInp, prefix);
        }
        // [AllureStep("Action: Click PREFIX dropdown")]
        // private async Task ActionClickOnPrefixDDL()
        // {
        //     await Click(prefixGeneratorDDL);
        // }
        [AllureStep("Assert: Category header should be displayed")]
        private async Task AssertPrefixGeneratorDDLIsDisplayed()
        {
            await ExpectElementToBeVisible(prefixGeneratorDDL);
        }
        [AllureStep("Assert: Category header should be displayed")]
        private async Task AssertCategoryHeaderIsDisplayed(string catName)
        {
            await ExpectElementToHaveText(catSettingHeaderLbl, catName);
        }
        [AllureStep("Action: Fill ADD NEW CATEGORY form")]
        private async Task ActionFillAddNewCatForm(string catName, string catOrd, string catType)
        {
            catName = catName.ToUpper();
            await ActionClickConfigIco();
            await ActionClickCategoryIco();
            await ActionClickAddNewCategoryBtn();
            await ActionFillCategoryNameInp(catName);
            await ActionFillDisplayOrderInp(catOrd);
            await ActionSelectCategoryTypeDDL(catType);
            await ActionClickNextBtn();
        }
        [AllureStep("Action: Click on ADD NEW CATEGORY button")]
        private async Task ActionClickNextBtn()
        {
            await Click(nextStepBtn);
        }
        [AllureStep("Action: Select CATEGORY TYPE dropdown")]
        private async Task ActionSelectCategoryTypeDDL(string catType)
        {
            await Click(selectCategoryTypeDDL);
            await Click(CategoryType(catType));
        }
        [AllureStep("Action: Fill DISPLAY ORDER input")]
        private async Task ActionFillDisplayOrderInp(string catOrd)
        {
            await Fill(displayOrderInp, catOrd);
        }
        [AllureStep("Action: Click on ADD NEW CATEGORY button")]
        private async Task ActionFillCategoryNameInp(string catName)
        {
            await Fill(categoryNameInp, catName);
        }
        [AllureStep("Action: Click on ADD NEW CATEGORY button")]
        private async Task ActionClickAddNewCategoryBtn()
        {
            await Click(addNewCategoryBtn);
        }
        [AllureStep("Action: Click on CATEGORY Icon")]
        private async Task ActionClickCategoryIco()
        {
            await Focus(categoryIco);
            await Wait(TimeoutUtils.MEDIUM_TIMEOUT);
            await Click(categoryIco);
        }
        [AllureStep("Action: Click on CONFIG Icon")]
        private async Task ActionClickConfigIco()
        {
            // await Focus(configurationIco);
            // await Hover(configurationIco);
            await Click(configurationIco);
            await Wait(TimeoutUtils.MEDIUM_TIMEOUT);
            // await Click(configurationIco);
        }

        [AllureStep("Action: Delete a Category bases on category name")]
        public async Task ActionDeleteCategoryByName(string CatName)
        {
            await ActionClickEditDeleteBtnBasedOnCatName(CatName);
            await ActionClickRemoveBtn();
            await AssertNewCatNameIsDisplayedOnTheConfirmPopup(CatName);
            await ActionClickYesRemoveCatNameOnRemoveConfirmationPopup();
            await ActionScrollingDown();
            await AssertNewCatNameIsNotDisplayedTbl(GetAddedCategoryFromTable(CatName));
        }
        [AllureStep("Assert: The inputted data has been removed successfully")]
        private async Task AssertNewCatNameIsNotDisplayedTbl(string CatName)
        {
            await ExpectElementNotToBeAttached(GetAddedCategoryFromTable(CatName));
        }

        [AllureStep("Action: Scrolling down to element")]
        private async Task ActionScrollingDown()
        {
            await ScrollDownToBottom(scrollableElementInsideCategoryTable);
        }

        [AllureStep("Assert: The confirmation message should contains a valid Category Name")]
        private async Task ActionClickYesRemoveCatNameOnRemoveConfirmationPopup()
        {
            await Click(yesRemoveCatNameOnRemoveConfirmationPopup);
        }

        [AllureStep("Assert: The confirmation message should contains a valid Category Name")]
        private async Task AssertNewCatNameIsDisplayedOnTheConfirmPopup(string CatName)
        {
            await ExpectElementToHaveText(CatNameOnRemoveConfirmationPopup, CatName);
        }

        [AllureStep("Action: Click Remove button")]
        private async Task ActionClickRemoveBtn()
        {
            await Click(RemoveBtn);
        }

        [AllureStep("Action: Click on Edit/Delete button base on Category name")]
        private async Task ActionClickEditDeleteBtnBasedOnCatName(string CatName)
        {
            await Click(GetEditDeleteButtonBasedOnCatName(CatName));
        }

        [AllureStep("Action: Delete a Category bases on category name")]
        public async Task ActionEditACategory(string CatName, string prefix)
        {
            string NewCatOrd = RandomGenerator.GetRandomNumberWithSpecificDigits(3);
            await ActionClickEditDeleteBtnBasedOnCatName(CatName);
            await ActionClickEditbtn();
            await ActionFillCategoryNameInp($"new_{CatName}");
            await ActionFillDisplayOrderInp(NewCatOrd);
            await ActionClickNextBtn();
            await AssertPrefixGeneratorDDLIsDisplayed();
            await ActionFillOnPrefixInp($"new_{prefix}");
            await AssertPrefixExampleNameIsDisplayed($"new_{prefix}");
            await ActionClickNextBtn();
            // await ActionFillManageWorkspaceTypeForm();
            await ActionFillManageMetadataForm();
            await ClickOnFinishBtn();
            await AssertNewCategoryIsAdded(CatName);
            await ActionDeleteCategoryByName(CatName);

        }

        [AllureStep("Action: Click Edit button")]
        public async Task ActionClickEditbtn()
        {
            await Click(EditBtn);
        }

        [AllureStep("Action: Delete Workspace by API")]
        public async Task API_DeleteWorkspace(string WorkspaceID)
        {
            // to be implemented 
        }
    }
}
