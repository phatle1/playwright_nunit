using Microsoft.Playwright;
using PortalTalk.AutomationTest.Utils;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages.UserApplication
{

    public class ActivityHistoryOverviewPage(IPage page) : BasePage(page)
    {

        private static string workspaceName(string val) => $"//span[contains(text(),'ACTIVITY HISTORY OVERVIEW')]/parent::div//span[contains(text(),'{val}')]";
        private static string workspaceType(string val) => $"//span[contains(text(),'ACTIVITY HISTORY OVERVIEW')]/parent::div//span[contains(@title,'{val}')]";
        private static string dateTime(string val) => $"//span[contains(text(),'ACTIVITY HISTORY OVERVIEW')]/parent::div//span[contains(text(),'{val}')]//parent::div//following::div[1]";


        [AllureStep("Assert: New Added Workspace is displayed correctly")]
        public async Task AssertIsNewWSAdded(string CatName, string submittedTime, string prefix)
        {
            string WorkspaceName = prefix + CatName;
            string WSInfo = "You just created workspace " + "\"" + WorkspaceName + "\"" + "";
            string WSType = $"{CatName} > {WorkspaceName}";

            var wsName = await GetInnerTextAsync(workspaceName(WSInfo));
            var wsType = await GetInnerTextAsync(workspaceType(WSType));
            var actualTime = await GetInnerTextAsync(dateTime(CatName));
            var isLessThan5Mins = AssertAbsoluteOfCreatedWSIsWithinFiveMinutes(actualTime, submittedTime);
            Assert.Multiple(() =>
            {
                Assert.That(wsName, Is.EqualTo(WSInfo));
                Assert.That(isLessThan5Mins, Is.EqualTo(true));
                Assert.That(wsType, Is.EqualTo(WSType));
            });

        }


        [AllureStep("Assert: Assert if the Created new Workspace time not exceed 5 minutes")]
        public bool AssertAbsoluteOfCreatedWSIsWithinFiveMinutes(string actual, string expected)
        {
            var actualDateTime = DateTimeUtils.StringToDateTime(actual);
            var expectedDateTime = DateTimeUtils.StringToDateTime(expected);
            TimeSpan val = actualDateTime - expectedDateTime;
            bool isLessThan5Mins = Math.Abs(val.TotalMinutes) <= 5;
            return isLessThan5Mins;
        }
    }
}
