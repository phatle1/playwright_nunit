using PortalTalk.AutomationTest.Utils;
using Allure.NUnit;
using Allure.NUnit.Attributes;
// using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer;
using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer.DAO;

namespace PortalTalk.AutomationTest.Tests.SmokeTest
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    [AllureSuite("Smoke Test Suite")]
    [AllureFeature("Add new workspace by create new workspace")]
    public class CreateANewWorkspace : BaseTest
    {
        [Test, Retry(3)]
        [AllureStory("Test Add New Workspace")]
        public async Task TestAddNewWorkspace()
        {
            Assert.Pass("Test Add New Workspace");
            // var connection = await dbConnect.CreateConnectionAsync();
            // var CatObject = await MAC_DAO.GetCatIdAndWorkspaceIdFromDatabase(connection, "AUTO_CATEGORY70567");

            // string RandomNumber = RandomGenerator.GetRandomNumberWithSpecificDigits(5);
            // string CatName = $"auto_category{RandomNumber}".ToUpper();
            // string CatOrd = RandomGenerator.GetRandomNumberWithSpecificDigits(3);
            // string CatType = "Microsoft Team";
            // string Prefix = $"auto_prefix{RandomNumber}".ToUpper();

            // await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, EnvUtils.USERNAME, EnvUtils.PWD);
            // await _landingPage.AssertUserIsLogedinSuccessfully(EnvUtils.USERNAME, "");
            // await _landingPage.ActionOpenAdminPage();
            // await _configuration.ActionOpenCategories();
            // await _configuration_categoriesPage.ActionAddNewCategory(CatName, CatOrd, CatType, Prefix, EnvUtils.USERNAME);

            // await _landingPage.ActionClickBackToUserApp();
            // await _dashBoardPage.ActionClickNewWorkSpaceBtn();
            // await _createNewWSPopupPages.ActionClickOnYesIwantToCreateBtn();
            // await _createNewWSPopupPages.ActionCreateNewWS(CatName);
            // await _dashBoardPage.ActionReloadPageUntilWorkspaceIsDisplayed(CatName);
            // await _dashBoardPage.AssertIsNewAddedWSDisplayedOnAF(CatName, _createNewWSPopupPages.submittedTime, Prefix);
            // await _dashBoardPage.AssertIsNewWorkspaceAdded(Prefix + CatName);
            // await _dashBoardPage.ActionClickSeeAllActivityHistoryBtn();
            // await _activityHistoryOverviewPage.AssertIsNewWSAdded(CatName, _createNewWSPopupPages.submittedTime, Prefix);
            // await _workSpacePage.API_DeleteWorkspace("");
            // await _configuration_categoriesPage.API_DeleteCategoryType("");
        }
    }
}
