using Microsoft.Playwright;
using NUnit.Framework;
using Allure.NUnit;
using System.Threading.Tasks;

using PortalTalk.AutomationTest.Pages.AdminCenter;
using PortalTalk.AutomationTest.Pages.UserApplication;
using PortalTalk.AutomationTest.Utils;
using PortalTalk.AutomationTest.Setups;
using PortalTalk.AutomationTest.Pages;
using Microsoft.Playwright.NUnit;
using Allure.Net.Commons;
using System.Text.Json;
using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer;
using PortalTalk.AutomationTest.BackendHelper.DatabaseLayer.DAO;

namespace PortalTalk.AutomationTest.Tests
{
    [TestFixture]
    public class BaseTest
    {
        private IPlaywright Playwright;
        private IBrowser Browser;
        private IBrowserContext Context;
        public IPage Page;

        public DatabaseSQLConnection dbConnect;
        public MAC_DAO _MAC_DAO;

        public LoginPage _loginPage { get; private set; }
        public DashboardPage _dashBoardPage { get; private set; }
        public WorkSpacePage _workSpacePage { get; private set; }
        public LandingPage _landingPage { get; private set; }
        public Configuration _configuration { get; private set; }
        public Categories _configuration_categoriesPage { get; private set; }
        public CreateNewWSPopups _createNewWSPopupPages { get; private set; }
        public ActivityHistoryOverviewPage _activityHistoryOverviewPage { get; private set; }

        protected virtual bool useLogin => true;
        [SetUp]
        public async Task EnvironmentSetup()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "dev";
            LoadEnv.Load(env);
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false

            });
            Context = await Browser.NewContextAsync();
            Context.SetDefaultTimeout(TimeoutUtils.MAX_TIMEOUT);
            Page = await Context.NewPageAsync();
            Page.SetDefaultTimeout(TimeoutUtils.MAX_TIMEOUT);

            dbConnect = new DatabaseSQLConnection();

            BasePage basePage = new(Page);

            // _MAC_DAO = new MAC_DAO();

            _loginPage = new LoginPage(Page);
            _dashBoardPage = new DashboardPage(Page);
            _workSpacePage = new WorkSpacePage(Page);
            _landingPage = new LandingPage(Page);
            _configuration = new Configuration(Page);
            _configuration_categoriesPage = new Categories(Page);
            _createNewWSPopupPages = new CreateNewWSPopups(Page);
            _activityHistoryOverviewPage = new ActivityHistoryOverviewPage(Page);

            if (useLogin)
            {
                // await _loginPage.LoginWithValidCredential(EnvUtils.BASE_URL, EnvUtils.USERNAME, EnvUtils.PWD);
                // string token = "";
                // string APIs_to_inspect = "https://portaltalknotificationtrunk.azurewebsites.net/hubs/activityFeed/negotiate";
                // token = await InspectAccessToken(APIs_to_inspect);
                // _loginPage.InspectTokenAndAddToEnv("dev", token);
            }
        }

        [TearDown]
        public async Task Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshotPath = "screenshot.png";
                await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
                AllureApi.AddAttachment("Screenshot on Failure", "image/png", screenshotPath);
            }
            if (Page != null)
            {
                await Page.CloseAsync();
                Page = null;
            }

            if (Context != null)
            {
                await Context.CloseAsync();
                Context = null;
            }
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            if (Browser != null)
            {
                await Browser.CloseAsync();
                Browser = null;
            }

            if (Playwright != null)
            {
                Playwright.Dispose();
                Playwright = null;
            }
        }

        public async Task<string> InspectAccessToken(string targetApiUrl)
        {
            string accessToken = "";
            try
            {
                await Page.ReloadAsync();
                var responseTask = await Page.WaitForResponseAsync(response =>
                    response.Url.Contains(targetApiUrl));
                var response = responseTask;
                var responseBody = await response.TextAsync();

                try
                {
                    var response_as_json = JsonDocument.Parse(responseBody);
                    var root = response_as_json.RootElement;
                    accessToken = root.GetProperty("accessToken").GetString() ?? "";
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing JSON response: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during token inspection: {ex.Message}");
                throw;
            }
            return accessToken;
        }
    }
}