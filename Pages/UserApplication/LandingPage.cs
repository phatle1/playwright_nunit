using Microsoft.Playwright;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.UserApplication
{
    public class LandingPage(IPage page) : BasePage(page)
    {
        private readonly string UserNameTxt = "(//div[@id='TopCommandBar']//div[contains(@class,'login-container')]//span)[2]";
        private readonly string EmailText = "(//div[@id='TopCommandBar']//div[contains(@class,'login-container')]//span)[3]";
        private readonly string personalItems = "(//div[@id='left-sidebar-container']//div[contains(@class,'ms-Nav-groupContent')])[1]//li";
        private readonly string loadingLbl = "//span[text()='Loading']";

        // portal talk admin center
        private readonly string potalTalkAdminPopup = "//*[contains(text(),'PortalTalk Admin Center')]";
        // private readonly ILocator settingBtn = LocatorUtils.GetElement("//*[@id='setting-group']");
        private readonly string settingBtn = "//div[@id='left-sidebar-container']//div[@role='button']/parent::div";
        private readonly string settingBtnFromAdminPage = "//i[@data-icon-name='Settings']/parent::span";
        private readonly string openAdminPageBtn = "//button[contains(text(), 'Open Admin Center')]";
        private readonly string backToUserAppbtn = "//button[contains(text(), 'Back To User Application')]";
        private readonly string standardScreenLbl = "//*[contains(text(),'Standard Screen')]";


        #region workspace in the left menu
        private readonly string workspaceContainerArea = "//div[@id='left-sidebar-container']//span[text()='Workspaces']/following-sibling::div";
        private string workspaceNamelbl(string catName) => $"//div[@id='left-sidebar-container']//span[text()='Workspaces']/following-sibling::div//div[contains(text(),'{catName}')]";
        private readonly string myLbl = "//div[@id='left-sidebar-container']//span[text()='Workspaces']//parent::div//div[@name='My']";
        private readonly string activitiesLbl = "//div[@id='left-sidebar-container']//div[contains(@name,'Activities')]";
        #endregion


        #region Add New Workspace
        private readonly string addNewWorkspacebtn = "//button//span[text()='New Workspace']";
        private readonly string workspaceNameInp = "//input[contains(@placeholder,'Tip: Choose a suitable and recognizable name for the workspace')]";
        private readonly string workspaceDescriptionAreaText = "//textarea[contains(@placeholder,'Add a description to make members understand this workspace')]";
        private readonly string publicCkb = "//span[text()='Public']";
        private readonly string privateCkb = "//span[text()='Private']";
        private readonly string ownerCbb = "//input[contains(@id,'combobox-id')]";
        private readonly string nextBtn = "//span[contains(text(),'Next')]";
        private readonly string cancelBtn = "//span[contains(text(),'Cancel')]";
        private readonly string okIUnderstandBtn = "//span[contains(text(),'OK, I understand')]";


        /*need to change back to this xpath, due to verification issue*/
        // private readonly string suggestedOwner = "//div[@dir='auto']//ancestor::button";
        private string suggestedOwner(string val) => $"//div[@dir='auto' and contains(text(),'{val}')]//ancestor::button";
        private readonly string finishBtn = "//span[contains(text(),'Finish')]";
        #endregion

        #region Your Pending Requests
        private string itemFromPendingRequestArea(string value) => $"//span[text()='Your Pending Requests']/parent::div//span[contains(text(),'{value}')]";
        #endregion


        #region Warning Before Create Workspace Popup
        private readonly string yesIWantCreateBtn = "//span[contains(text(),'Yes, I want to create a new one')]";
        private readonly string noThanksBtn = "//span[contains(text(),'No thanks')]";
        #endregion

        #region New Workspace Approval 
        private readonly string approveBtn = "//button[text()='Approve']";
        private readonly string rejectBtn = "//button[text()='Reject']";
        private readonly string approvalDescriptionTxtarea = "//span[text()='You Have Approved']/parent::div//textarea";
        private readonly string yesSendMyCommentBtn = "//button[contains(text(),'Yes, Send My Comment')]";
        private readonly string youHaveApprovedLbl = "//span[contains(text(),'You Have Approved')]";
        private readonly string okDoneBtn = "//button[contains(text(),'Ok, done')]";
        #endregion


        #region My Workspace
        private string settingButtonFromMyWorkspace(string wsName) => $"//div[contains(text(),'{wsName}')]/parent::div/following-sibling::div/button";
        private readonly string manageWorkspaceMemberLbl = "//span[contains(text(),'Manage Members')]";
        private string memberInWorkspace(string member) => $"//h3[contains(text(),'Internal members')]/parent::div/following-sibling::div//div[contains(text(),'{member}')]";
        #endregion


        [AllureStep("Action: Click on SETTING btn")]
        public async Task ActionClickOnSettingBtn()
        {
            await Click(settingBtn);
        }

        [AllureStep("Action: Click on SETTING on Admin Page btn")]
        public async Task ActionClickOnSettingBtnFromAdminPage()
        {
            await Click(settingBtnFromAdminPage);
        }

        [AllureStep("Assert: Portaltalk admin popup should be displayed")]
        public async Task AssertPortalTalkAdminPopupIsDisplayed()
        {
            await ExpectElementToBeVisible(potalTalkAdminPopup);
            await ExpectElementToBeVisible(standardScreenLbl);
            await ExpectElementToBeVisible(openAdminPageBtn);
        }

        [AllureStep("Action: Click on OPEN ADMIN CENTER btn")]
        public async Task ActionClickOnOpenAdminCenterBtn()
        {
            await Click(openAdminPageBtn);
        }

        [AllureStep("Action: Click on Back To User Application btn")]
        public async Task ActionClickBackToUserAppbtn()
        {
            await Click(backToUserAppbtn);
        }

        [AllureStep("Action: Open Admin page")]
        public async Task ActionOpenAdminPage()
        {
            await ActionClickOnSettingBtn();
            await AssertPortalTalkAdminPopupIsDisplayed();
            await ActionClickOnOpenAdminCenterBtn();
        }

        [AllureStep("Assert: the correct EMAIL should be displayed")]
        public async Task AssertUserIsLogedinSuccessfully(string email, string userName)
        {
            // Vinh is updating on this UI
            await ExpectElementToHaveText(EmailText, email);
            // await ExpectElementToHaveText(UserNameTxt, userName);
        }

        [AllureStep("Action: click Back to 'User Application' screen")]
        public async Task ActionClickBackToUserApp()
        {
            await ActionClickOnSettingBtnFromAdminPage();
            await ActionClickBackToUserAppbtn();
        }

        #region workspace section
        [AllureStep("Action: Click 'Workspace'")]
        public async Task ActionClickToMyWorkspace(string catName)
        {
            await Click(workspaceNamelbl(catName));
        }

        [AllureStep("Action: Click 'My' lbl'")]
        public async Task ActionClickToMyLbl()
        {
            await Click(myLbl);
        }

        [AllureStep("Action: Click 'Yes, I want to create a new one' button'")]
        public async Task ActionClickConfirmPopupIfShown()
        {
            bool isWarningPopupDisplayed = await IsElementDisplayed(yesIWantCreateBtn);
            if (isWarningPopupDisplayed)
            {
                await Click(yesIWantCreateBtn);
            }
        }

        [AllureStep("Action: Fill 'Workspace name' input'")]
        public async Task ActionFillWorkspaceNameInp(string workspaceName)
        {
            await Fill(workspaceNameInp, workspaceName);
        }

        [AllureStep("Action: Fill 'Description' input'")]
        public async Task ActionFillDescription(string desc)
        {
            await Click(workspaceDescriptionAreaText);
            await Fill(workspaceDescriptionAreaText, desc);
        }

        [AllureStep("Action: Click 'Private' Checkbob'")]
        public async Task ActionClickToPrivateCkb()
        {
            await Click(privateCkb);
        }

        [AllureStep("Action: Fill 'Owners' combobox")]
        public async Task ActionFillToOwnersCbb(string owner)
        {
            await Click(ownerCbb);
            await Fill(ownerCbb, owner);
            await Click(suggestedOwner(owner));
        }

        [AllureStep("Action: Click 'Next' button'")]
        public async Task ActionClickToNextBtn()
        {
            await Click(nextBtn);
        }

        [AllureStep("Action: Click 'Finish' button'")]
        public async Task ActionClickFinishBtn()
        {
            await Click(finishBtn);
        }

        [AllureStep("Action: Click 'New Workspace' button'")]
        public async Task ActionClickToAddNewWorkspacebtn()
        {
            await Click(addNewWorkspacebtn);
        }

        [AllureStep("Action: Click 'Approve' button'")]
        public async Task ActionClickApproveBtn()
        {
            await Click(approveBtn);
        }
        [AllureStep("Action: Fill 'Approve Description' text area")]
        public async Task ActionFillApproveDescriptionTxtarea(string val)
        {
            await Fill(approvalDescriptionTxtarea, val);
        }

        [AllureStep("Action: Click 'Yes, Send My Comment' button")]
        public async Task ActionClickYesSendMyCommentBtn()
        {
            await Click(yesSendMyCommentBtn);
        }

        [AllureStep("Action: Click 'Ok, done' button")]
        public async Task ActionClickOkDoneBtn()
        {
            await Click(okDoneBtn);
        }

        [AllureStep("Action: Click item from 'Your Pending Requests' list'")]
        public async Task ActionClickItemFromPendingRequest(string item)
        {
            await Click(itemFromPendingRequestArea(item));
        }

        [AllureStep("Action: Click 'OK, I understand' button")]
        public async Task ActionClickOkIUnderstandBtn()
        {
            await Click(okIUnderstandBtn);
        }

        [AllureStep("Action: Sroll down to workspace")]
        public async Task ActionScrolldownToElementAndSelectMyWorkspace(string catName)
        {   /* temporary comment out the following: need to change xpath to css  */
            // await ScrollDownByKeyboardUntilElement(workspaceNamelbl(catName), workspaceContainerArea);
            await ActionClickToMyWorkspace(catName);
            await ActionClickToMyLbl();
        }

        [AllureStep("Action: Select The Created Workspace")]
        public async Task SelectTheCreatedWorkspace(string catname, string workspaceName, string description, string ownerName)
        {
            await ActionScrolldownToElementAndSelectMyWorkspace(catname);
            await ActionClickToAddNewWorkspacebtn();
            await ActionClickConfirmPopupIfShown();
            await ActionFillWorkspaceNameInp(workspaceName);
            await ActionFillDescription(description);
            await ActionFillToOwnersCbb(ownerName);
            await ActionClickToPrivateCkb();
            await ActionClickToNextBtn();
            await ActionClickFinishBtn();
            await ActionClickOkIUnderstandBtn();
        }

        [AllureStep("Action: Select The Item from Your Pending Requests and approve it")]
        public async Task ActionSelectAndApproveAWorkspaceFromYourPendingRequests(string item, string description)
        {
            // item = "Phat_test_test2126";
            await Click(activitiesLbl);
            await ActionClickItemFromPendingRequest(item);
            await ExpectElementToBeVisible(loadingLbl);
            await ActionClickApproveBtn();
            await ActionFillApproveDescriptionTxtarea(description);
            await ActionClickYesSendMyCommentBtn();
            await ExpectElementToBeVisible(loadingLbl);
            await ExpectElementToBeVisible(youHaveApprovedLbl);
            await ActionClickOkDoneBtn();
        }
        [AllureStep("Assert: Is new member added successfully")]
        public async Task AssertNewMemberIsAddedSuccessfully(string catname, string workspaceName, string member)
        {
            await ActionScrolldownToElementAndSelectMyWorkspace(catname);
            await Click(settingButtonFromMyWorkspace(workspaceName));
            await Click(manageWorkspaceMemberLbl);
            await ExpectElementToBeVisible(memberInWorkspace(member));
        }

        #endregion
    }
}
