using Microsoft.Playwright;
using PortalTalk.AutomationTest.Setups;
using PortalTalk.AutomationTest.Utils;
using System.Threading.Tasks;
using AllureStepAttribute = Allure.NUnit.Attributes.AllureStepAttribute;

namespace PortalTalk.AutomationTest.Pages
{
    public class LoginPage(IPage page) : BasePage(page)
    {
        private readonly string userNameInp = "//input[@type='email']";
        private readonly string passWordInp = "//input[@type='password']";
        private readonly string AccountLbl = "//*[@id='displayName']";
        private readonly string submitBtn = "//input[@type='submit']";
        private readonly string yesBtn = "//*[@value='Yes']";

        [AllureStep("Action: Navigate to PortalTalk url")]
        public async Task NavigateToURL(string URL)
        {
            await GoTo(URL);
        }
        [AllureStep("Action: Fill User Name txt")]
        public async Task FillUserName(string username)
        {
            await Fill(userNameInp, username);
        }
        [AllureStep("Action: Fill Password txt")]
        public async Task FillPassword(string pwd)
        {
            await Fill(passWordInp, pwd);
        }
        [AllureStep("Action: Click Next btn")]
        public async Task ClickNextBtn()
        {
            await Click(submitBtn);
        }
        [AllureStep("Action: Click YES btn")]
        public async Task ClickYesBtn()
        {
            await Click(yesBtn);
            await FullWait();
            // await Reload(1);
            // await FullWait();
        }

        [AllureStep("Step: Login PortalTalk with a valid credential")]
        public async Task LoginWithValidCredential(string URL, string username, string password)
        {
            await NavigateToURL(URL);
            await FillUserName(username);
            await ClickNextBtn();
            await FillPassword(password);
            await ClickNextBtn();
            await ClickYesBtn();
        }

        [AllureStep("Step: Inspect ACCESS_TOKEN and add to .ENV file")]
        public void InspectTokenAndAddToEnv(string env, string token)
        {
            UpdateAccessToken(env, token);
        }
    }
}
