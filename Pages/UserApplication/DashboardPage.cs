using System.Globalization;
using Microsoft.Playwright;
using PortalTalk.AutomationTest.Utils;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.UserApplication
{
    public class DashboardPage(IPage page) : BasePage(page)
    {
        private readonly string dashBoardHeaderLbl = "//*[@id='workspace-content']//span[contains(text(),'Dashboard')]";
        private readonly string currentDateTimeLbl = "//*[@id='workspace-content']//span[contains(text(),'Dashboard')]//following-sibling::span";
        private readonly string newWorkSpaceBtn = "//button[text()='New Workspace']";
        private readonly string seeActivityHistory = "//button[contains(text(),'See Activity History')]";


        #region Activity Feed section
        private string workspaceNamelbl(string catName) => $"//div[@id='left-sidebar-container']//span[text()='Workspaces']/following-sibling::div//div[contains(text(),'{catName}')]";
        private string listItemsFromAFByCatName(string val) => $"//span[text()='{val}']//ancestor::div[contains(@class,'timeline-item-text')]/div";
        private string newWSAddedFromNewWorkSpacesSection(string val) => $"//span[@title='{val}']";

        private string newRequestedWorkspace(string val) => $"//div[contains(text(),'Your requested workspace '{val}'')]//ancestor::div[@class='timeline-item']";
        #endregion

        #region Add new member to workspace
        private string workspaceNameTitle(string val) => $"//span[contains(text(),'{val}') and not(@title)]";
        private readonly string addNewMemberBtn = "//button[contains(text(),'Add New Members')]";
        private readonly string addMemberInp = "//input[contains(@placeholder,'Find and select a user ...')]";
        private readonly string addMemberBtn = "//button[contains(text(),'Add members')]";
        private readonly string addingMemberLbl = "//span[contains(text(),'ADDING MEMBERS')]";
        private readonly string gotoManageMemberBtn = "//span[contains(text(),'CONGRATULATION')]//following-sibling::div/button[text()='Go to Manage Members']";
        private readonly string backToYourWorkspaceBtn = "//span[contains(text(),'CONGRATULATION')]//following-sibling::div/button[text()='Back to your workspace']";
        private readonly string firstMemberNameFromSuggestedUserLbl = "(//div[contains(text(),'Suggested users')]/following-sibling::div//div//div[@dir='auto'])[1]";
        private readonly string firstMemberEmailFromSuggestedUserLbl = "(//div[contains(text(),'Suggested users')]/following-sibling::div//div//div[@dir='auto'])[2]";
        #endregion


        /*Functions*/
        [AllureStep("Action: Click on Add New Workspace btn")]
        public async Task ActionClickNewWorkSpaceBtn()
        {
            await FullWait();
            await Click(newWorkSpaceBtn);
        }

        [AllureStep("Action: Click on Add New Workspace btn")]
        public async Task ActionClickSeeAllActivityHistoryBtn()
        {
            await Click(seeActivityHistory);
        }

        [AllureStep("Action: Reload until new Workspace is displayed")]
        public async Task ActionReloadPageUntilWorkspaceIsDisplayed(string CatName)
        {
            await ReloadUntilElementFound(listItemsFromAFByCatName(CatName));
        }

        [AllureStep("Assert: Inputted Workspace is displayed on Activity Feed")]
        public async Task AssertIsNewAddedWSDisplayedOnAF(string CatName, string submittedTime, string prefix)
        {
            string WorkspaceName = prefix + CatName;
            string[] expected = [submittedTime,
            $"You just created workspace \"{WorkspaceName}\"",
            $"{CatName} > {WorkspaceName}"];
            var elements_from_feed = await GetCountAsync(listItemsFromAFByCatName(CatName));
            while (elements_from_feed < 1)
            {
                await ReloadUntilElementFound(listItemsFromAFByCatName(CatName), 1);
                elements_from_feed = await GetCountAsync(listItemsFromAFByCatName(CatName));
            }
            var AFItems = await GetElement(listItemsFromAFByCatName(CatName));
            int count = await GetCountAsync(listItemsFromAFByCatName(CatName));
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    var isLessThan5Mins = AssertCreatedTimeWithin5Mins(await AFItems.Nth(i).InnerTextAsync(), expected[i]);
                    Assert.That(isLessThan5Mins, Is.EqualTo(true));
                }
                else
                {
                    var element = AFItems.Nth(i);
                    string text = await element.InnerTextAsync();
                    var isEqual = IsEqual(text, expected[i]);
                    Assert.That(isEqual, Is.EqualTo(true));
                }
            }
        }

        [AllureStep("Assert: Assert is new Workspace added to 'NEW WORKSPACES' section")]
        public async Task AssertIsNewWorkspaceAdded(string WSName)
        {
            bool check = true;
            int maxAttempts = 0;
            while (check && maxAttempts < 10)
            {
                try
                {
                    var isDisplayed = await IsElementDisplayed(newWSAddedFromNewWorkSpacesSection(WSName));
                    if (isDisplayed == true)
                    {
                        Assert.That(isDisplayed, Is.EqualTo(true));
                        check = false;
                    }
                    else
                    {
                        await Reload(1);
                        maxAttempts++;
                    }
                }
                catch (System.Exception)
                {
                    await Reload(1);
                    maxAttempts++;
                }
            }
        }

        [AllureStep("Assert: Assert if the Created new Workspace time not exceed 5 minutes")]
        public static bool AssertCreatedTimeWithin5Mins(string actual, string expected)
        {
            var actualDateTime = DateTimeUtils.StringToDateTime(actual);
            var expectedDateTime = DateTimeUtils.StringToDateTime(expected);
            TimeSpan val = actualDateTime - expectedDateTime;
            bool isLessThan5Mins = Math.Abs(val.TotalMinutes) <= 5;
            return isLessThan5Mins;
        }

        [AllureStep("Assert: Assert if the Created new Workspace time not exceed 5 minutes")]
        public async Task ActionAddMemberToTheCreatedWorkspaceOnActivityFeed(string workspaceName, string expected)
        {
            await ExpectElementToBeVisible(workspaceNameTitle(workspaceName));
        }

        [AllureStep("Action: Action select and add member your requested workspace from Activity Feed")]
        public async Task ActionSelectYourRequestedWorkSpaceAndAddMemberFromActivityFeed(string workspaceName, string newMember)
        {
            await Click(newRequestedWorkspace(workspaceName));
            await Click(addNewMemberBtn);
            await Fill(addMemberInp, newMember);
            await Click(addMemberBtn);
            await ExpectElementToBeVisible(addingMemberLbl);
            await ExpectElementToBeVisible(gotoManageMemberBtn);
            await ExpectElementToBeVisible(backToYourWorkspaceBtn);
        }
    }
}
