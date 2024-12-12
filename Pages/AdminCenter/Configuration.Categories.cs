using Microsoft.Playwright;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.AdminCenter
{


    public class Categories(IPage page) : BasePage(page)
    {
        #region Category Information
        public readonly string addNewCategoryBtn = "//*[contains(text(),'Add New Category')]";
        public readonly string categoryNameInp = "//*[@placeholder='Please enter name of category']";
        public readonly string displayOrderInp = "//*[text()='Display Order']//following-sibling::span//input";
        public readonly string selectCategoryTypeDDL = "//*[text()='Select category type']/following-sibling::div//button[1]";
        #endregion

        public readonly string nextStepBtn = "//button[text()='Next Step']";
        public readonly string prefixGeneratorDDL = "//span[contains(text(),'Prefix generator')]";
        public readonly string prefixGeneratorInp = "//input[contains(@id,'TextField')]";
        public readonly string exampleNameTxt = "(//input[contains(@id,'TextField')]//ancestor::div[contains(@class,'pt-text-field-container')]//following::div[3])[1]";
        public readonly string addNewWorkSpaceTypeBtn = "//button[contains(text(),'Add New Workspace Type')]";
        public readonly string workspaceTypeNameInp = "//input[contains(@placeholder,'Please enter workspace type name')]";

        /*Workspace creation*/
        public readonly string groupAndUserCreatePermissionInp = "//label[contains(text(),'Create permissions')]//following-sibling::div//input";
        public readonly string groupAndUserRequestPermissionInp = "//label[contains(text(),'Request permissions')]//following-sibling::div//input";
        public readonly string createDirecltyOnlyChkbox = "//label[text()='Workspace creation']//following::input[1]";
        public readonly string saveNewWorkspaceTypeBtn = "//button[contains(text(),'Save New W')]";
        public readonly string addNewMetadataBtn = "//button[contains(text(),'Add New Metadata')]";
        public readonly string finishBtn = "//button[contains(text(),'Finish')]";
        public readonly string loadingTxt = "//div/span[contains(text(),'LOADING')]";
        public readonly string catCreatedSuccessfullyTxt = "//span[contains(text(),'The Category created successfully')]";
        public readonly string firstItemOfTable = "//div[@role='presentation' and @class='ms-DetailsList-contentWrapper']//div[@data-list-index='0']";
        public readonly string scrollableElementInsideCategoryTable = "div[data-is-scrollable='true']";
        public readonly string catNameOnRemoveConfirmationPopup = "//div[contains(text(),'Remove Category')]//following-sibling::div//span";
        public readonly string yesRemoveCatNameOnRemoveConfirmationPopup = "//button[contains(text(),'Yes, Remove')]";

        /*temporary comment this line*/
        // public readonly string addNewWorkSpaceType_userSuggestionBtn = "//div[@dir='auto']//ancestor::button";
        public static string addNewWorkSpaceType_userSuggestionBtn(string userName) => $"//div[@dir='auto']//ancestor::button//div[text()='{userName}']";

        public readonly string ScrollableElementInsideCategoryTable = "div[data-is-scrollable='true']";

        // public readonly string catSettingHeaderLbl = "//span[contains(text(),'ADD NEW CATEGORY')]";
        public static string CatSettingHeaderLbl(string val) => $"//span[contains(text(),'{val}')]";
        public static string CategoryType(string val) => $"//*[contains(@id,'fluent-option') and contains(text(),'{val}')]";
        public static string GetAddedCategoryFromTable(string catName) => $"//div[contains(@class,'ScrollablePane')]//div[@role='gridcell'][3]//span[contains(text(),'{catName}')]";
        public static string GetEditDeleteButtonBasedOnCatName(string catName) => $"//span[contains(text(),'{catName}')]//ancestor::div[@data-automationid='ListCell']//button";


        [AllureStep("Assert: Assert Add New Category Page is displayed")]
        public async Task AssertAddNewCatPageIsDisplayed()
        {
            await ExpectElementToBeVisible(categoryNameInp);
        }

        [AllureStep("Action: Click on Add New Category Btn")]
        public async Task ActionClickAddNewCategoryBtn()
        {
            await Click(addNewCategoryBtn);
        }

        [AllureStep("Action: Fill Catrgory Name")]
        public async Task ActionFillCategoryNameInp(string catName)
        {
            await Fill(categoryNameInp, catName);
        }

        [AllureStep("Action: Fill DISPLAY ORDER input")]
        public async Task ActionFillDisplayOrderInp(string catOrd)
        {
            await Fill(displayOrderInp, catOrd);
        }

        [AllureStep("Action: Select CATEGORY TYPE dropdown")]
        public async Task ActionSelectCategoryTypeDDL(string catType)
        {
            await Click(selectCategoryTypeDDL);
            await Click(CategoryType(catType));
        }

        [AllureStep("Action: Click on NEXT button")]
        public async Task ActionClickNextBtn()
        {
            await Click(nextStepBtn);
        }

        [AllureStep("Assert: Category header should be displayed")]
        public async Task AssertCategoryHeaderIsDisplayed(string catName)
        {
            var text = await IsElementDisplayed(CatSettingHeaderLbl(catName));
            Assert.That(text, Is.EqualTo(true));
        }

        [AllureStep("Assert: Category header should be displayed")]
        public async Task AssertPrefixGeneratorDDLIsDisplayed()
        {
            var isDisplayed = await IsElementDisplayed(prefixGeneratorDDL);
            Assert.That(isDisplayed, Is.EqualTo(true));
        }

        [AllureStep("Assert: WORKSPACE PREFIX EXAMPLE should be displayed")]
        public async Task AssertPrefixExampleNameIsDisplayed(string prefix)
        {
            var text = await GetInnerTextAsync(exampleNameTxt);
            Assert.That(text, Does.Contain(prefix));
        }

        [AllureStep("Assert: Prefix MANAGE WORK SPACE should be displayed")]
        public async Task AssertManageWorkSpaceTypeIsDisplayed()
        {
            var isDisplayed = await IsElementDisplayed(addNewWorkSpaceTypeBtn);
            Assert.That(isDisplayed, Is.EqualTo(true));
        }

        [AllureStep("Action: Click on Add New Workspace Type button")]
        public async Task ClickAddNewWorkspaceTypeBtn()
        {
            await Click(addNewWorkSpaceTypeBtn);
        }

        [AllureStep("Action: Enable Create Direclty Only Checkbox")]
        public async Task ClickCreateDirectlyOnlyCheckbox()
        {
            await Click(createDirecltyOnlyChkbox);
        }

        [AllureStep("Action: Fill to Category Type Name")]
        public async Task FillCatTypeName(string catName)
        {
            await Fill(workspaceTypeNameInp, catName);
        }

        [AllureStep("Action: Fill User Group Permission")]
        public async Task FillUserGroupCreatePermission(string user)
        {
            await Fill(groupAndUserCreatePermissionInp, user);
        }
        [AllureStep("Action: Fill User Group Permission With Request Permission and Create Permission")]
        public async Task FillUserGroupRequestPermission(string user)
        {

            await Fill(groupAndUserRequestPermissionInp, user);
        }


        [AllureStep("Action: Click on the Suggested user name button")]
        public async Task ClickSuggestedUserName(string userName)
        {
            await Click(addNewWorkSpaceType_userSuggestionBtn(userName));
        }

        [AllureStep("Action: Click on Save New Workspace Type button")]
        public async Task ClickSaveNewWorkspaceTypeBtn()
        {
            await Click(saveNewWorkspaceTypeBtn);
        }

        [AllureStep("Assert: ADD NEW METADATA button should be displayed")]
        public async Task AssertAddNewMetadataBtn()
        {
            await ExpectElementToBeVisible(addNewMetadataBtn);
        }

        [AllureStep("Action: Fill PREFIX inp")]
        public async Task ActionFillOnPrefixInp(string prefix)
        {
            await Click(prefixGeneratorDDL);
            await Fill(prefixGeneratorInp, prefix);
        }

        [AllureStep("Action: Fill 'MANAGE METADATA' form")]
        public async Task ActionFillManageMetadataForm()
        {
            await AssertAddNewMetadataBtn();
        }

        [AllureStep("Action: Click on FINISH button")]
        public async Task ClickOnFinishBtn()
        {
            await Click(finishBtn);
        }

        [AllureStep("Assert: LOADING popup should be displayed")]
        public async Task AssertLoadingPopupIsDisplayed()
        {
            await ExpectElementToBeVisible(loadingTxt);
        }

        [AllureStep("Assert: The new Category should be added to the table")]
        public async Task AssertAddNewCatTxtIsDisplayed()
        {
            await ExpectElementToBeVisible(catCreatedSuccessfullyTxt);
        }

        [AllureStep("Assert: The new Category should be added to the table")]
        public async Task AssertNewCategoryIsAdded(string catName)
        {
            await Click(firstItemOfTable);
            await ScrollDownByKeyboardUntilElement(GetAddedCategoryFromTable(catName), ScrollableElementInsideCategoryTable);
            var isDisplayed = await IsElementDisplayed(GetAddedCategoryFromTable(catName));
            Assert.That(isDisplayed, Is.EqualTo(true));
        }

        [AllureStep("Action: Fill 'MANAGE WORKSPACE TYPES' form with Create Permission only")]
        public async Task ActionFillManageWorkspaceTypeFormWithCreatePermission(string WorkspaceTypeName, string userNameForCreatePermission)
        {
            await AssertManageWorkSpaceTypeIsDisplayed();
            await ClickAddNewWorkspaceTypeBtn();
            await FillCatTypeName(WorkspaceTypeName);
            await FillUserGroupCreatePermission(userNameForCreatePermission);
            await ClickSuggestedUserName(userNameForCreatePermission);
            await ClickSaveNewWorkspaceTypeBtn();
            await ActionClickNextBtn();
        }
        [AllureStep("Action: Fill 'MANAGE WORKSPACE TYPES' form with Create Permission and Request Permission")]
        public async Task ActionFillManageWorkspaceTypeFormWithRequestAndCreatePermission(string WorkspaceTypeName, string userNameForRequestPermission, string userNameForCreatePermission)
        {
            await AssertManageWorkSpaceTypeIsDisplayed();
            await ClickAddNewWorkspaceTypeBtn();
            await FillCatTypeName(WorkspaceTypeName);

            await ClickCreateDirectlyOnlyCheckbox();

            await FillUserGroupRequestPermission(userNameForRequestPermission);
            await ClickSuggestedUserName(userNameForRequestPermission);

            await FillUserGroupCreatePermission(userNameForCreatePermission);
            await ClickSuggestedUserName(userNameForCreatePermission);

            await ClickSaveNewWorkspaceTypeBtn();
            await ActionClickNextBtn();
        }

        [AllureStep("Action: Fill ADD NEW CATEGORY form")]
        public async Task ActionFillAddNewCatForm(string catName, string catOrd, string catType)
        {
            catName = catName.ToUpper();
            await ActionClickAddNewCategoryBtn();
            await AssertAddNewCatPageIsDisplayed();
            await ActionFillCategoryNameInp(catName);
            await ActionFillDisplayOrderInp(catOrd);
            await ActionSelectCategoryTypeDDL(catType);
            await ActionClickNextBtn();
        }

        [AllureStep("Action: Fill 'CATEGORY SETTINGS' form")]
        public async Task ActionFillCatSettingsForm(string catName, string prefix)
        {
            await AssertCategoryHeaderIsDisplayed($"ADD NEW CATEGORY: {catName}");
            await AssertPrefixGeneratorDDLIsDisplayed();
            await ActionFillOnPrefixInp(prefix);
            await AssertPrefixExampleNameIsDisplayed(prefix);
            await ActionClickNextBtn();
        }

        [AllureStep("Action: Add New Category")]
        public async Task ActionAddNewCategory(string catName, string catOrd, string catType, string prefix, string userName)
        {
            await ActionFillAddNewCatForm(catName, catOrd, catType);
            await ActionFillCatSettingsForm(catName, prefix);
            await ActionFillManageWorkspaceTypeFormWithCreatePermission(catName, userName);
            await ActionFillManageMetadataForm();
            await ClickOnFinishBtn();
            await AssertLoadingPopupIsDisplayed();
            await AssertAddNewCatTxtIsDisplayed();
            await AssertNewCategoryIsAdded(catName);
        }

        [AllureStep("Action: Add New Category With Request And Create Permission For Workspace Type")]
        public async Task ActionAddNewCategoryWithCreateAndRequestForWorkspaceType(string catName, string catOrd, string catType, string prefix, string userNameRequest, string userNameCreate)
        {
            await ActionFillAddNewCatForm(catName, catOrd, catType);
            await ActionFillCatSettingsForm(catName, prefix);
            await ActionFillManageWorkspaceTypeFormWithRequestAndCreatePermission(catName, userNameRequest, userNameCreate);
            await ActionFillManageMetadataForm();
            await ClickOnFinishBtn();
            await AssertLoadingPopupIsDisplayed();
            await AssertAddNewCatTxtIsDisplayed();
            await AssertNewCategoryIsAdded(catName);
        }



        [AllureStep("Action: Delete Category Type by API")]
        public async Task API_DeleteCategoryType(string CatType)
        {
            // to be implemented 
            await Task.CompletedTask;
        }
    }
}
