using PortalTalk.AutomationTest.Utils;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Newtonsoft.Json;
using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer.DAO;

namespace PortalTalk.AutomationTest.Tests.SmokeTest
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    [AllureSuite("Smoke Test Suite")]
    [AllureFeature("Test request workspace by request permission")]
    [Ignore("Ignoring this test class temporarily")]
    public class CreateANewWorkspaceByRequestPermission : BaseTest
    {
        private static readonly string RandomNumber = RandomGenerator.GetRandomNumberWithSpecificDigits(5);
        private static readonly string CatName = $"auto_category{RandomNumber}".ToUpper();
        private static readonly string CatOrd = RandomGenerator.GetRandomNumberWithSpecificDigits(3);
        private static readonly string CatType = "Microsoft Team";
        private static readonly string Prefix = $"auto_prefix{RandomNumber}".ToUpper();
        private static readonly string WorkspaceName = $"workspace_{CatName}";
        private static readonly string description = $"description_{CatName}";

        // static CreateANewWorkspaceByRequestPermission()
        // {
        //     RandomNumber = RandomGenerator.GetRandomNumberWithSpecificDigits(5);
        //     CatName = $"auto_category{RandomNumber}".ToUpper();
        //     CatOrd = RandomGenerator.GetRandomNumberWithSpecificDigits(3);
        //     CatType = "Microsoft Team";
        //     Prefix = $"auto_prefix{RandomNumber}".ToUpper();
        //     WorkspaceName = $"workspace_{CatName}";
        //     description = $"description_{CatName}";
        // }

        [Test, Order(1), Retry(3)]
        [AllureStory("Test add new category and add new workspace type")]
        public async Task TestAddNewCategoryAndNewWorkspaceType()
        {
            string USERNAME = "admin@alteraiam.onmicrosoft.com";
            string PASSWORD = "Welkom0202";
            await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, USERNAME, PASSWORD);
            await _landingPage.AssertUserIsLogedinSuccessfully(USERNAME, "");
            await _landingPage.ActionOpenAdminPage();
            await _configuration.ActionOpenCategories();
            await _configuration_categoriesPage.ActionAddNewCategoryWithCreateAndRequestForWorkspaceType(CatName, CatOrd, CatType, Prefix, "Gary@alteraiam.com", "Belinda@alteraiam.com");
        }

        [Test, Order(2), Retry(3)]
        [AllureStory("Test request a workspace")]
        public async Task TestRequestAWorkspace()
        {
            string USERNAME = "Gary@alteraiam.com";
            string PASSWORD = "Welkom0202";
            await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, USERNAME, PASSWORD);
            await _landingPage.AssertUserIsLogedinSuccessfully(USERNAME, "");
            await _landingPage.SelectTheCreatedWorkspace(CatName, WorkspaceName, description, USERNAME);
        }

        [Test, Order(3), Retry(3)]
        [AllureStory("Test workspace approval")]
        public async Task TestWorkspaceApproval()
        {
            // string CatName = "AUTO_CATEGORY19917";
            string USERNAME = EnvUtils.USERNAME;
            string PASSWORD = EnvUtils.PWD;
            await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, USERNAME, PASSWORD);
            await _landingPage.AssertUserIsLogedinSuccessfully(USERNAME, "");
            await _landingPage.ActionSelectAndApproveAWorkspaceFromYourPendingRequests(WorkspaceName, description);
        }

        [Test, Order(4), Retry(3)]
        [AllureStory("Test add member to workspace")]
        public async Task TestAddMember()
        {

            var connection = await dbConnect.CreateConnectionAsync();
            var macDao = new MAC_DAO();
            // var CatObject = await macDao.GetCatIdAndWorkspaceIdFromDatabase(connection, "AUTO_CATEGORY70567");
            string userAsJson = await macDao.GetTop1UnblockedUser(connection);
            var userAsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(userAsJson);

            #region need to double check
            var email = "";
            // if (userObject != null)
            // {
            //     foreach (var kvp in userObject)
            //     {
            //         if (kvp.Value is Dictionary<string, object> nestedDictionary)
            //         {
            //             foreach (var nestedKvp in nestedDictionary)
            //             {
            //                 var tempKey = nestedKvp.Key;
            //                 var tempVal = nestedKvp.Value;
            //                 if (tempKey == "Email")
            //                 {
            //                     email = tempVal.ToString();
            //                 }
            //             }
            //         }
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("userObject is not a Dictionary<string, object>");
            // }
            #endregion

            string USERNAME = "Gary@alteraiam.com";
            string PASSWORD = "Welkom0202";
            await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, USERNAME, PASSWORD);
            await _landingPage.AssertUserIsLogedinSuccessfully(USERNAME, "");

            //need to get user from DB and double check if user is blocked
            await _dashBoardPage.ActionSelectYourRequestedWorkSpaceAndAddMemberFromActivityFeed(WorkspaceName, email);
            //need to add assert
            await _landingPage.AssertNewMemberIsAddedSuccessfully(WorkspaceName, "", " ");
        }
    }
}
