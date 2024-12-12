using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;
using System.Threading.Tasks;

namespace PortalTalk.AutomationTest.Pages
{
    // Base Steps class containing common functionality
    public class BaseSteps
    {
        protected IAPIRequestContext Request;
        protected readonly string BaseUrl = "https://api.example.com";
        protected JsonSerializerOptions JsonOptions;

        public BaseSteps()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task InitializeApiContext()
        {
            var playwright = await Playwright.CreateAsync();
            Request = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = BaseUrl,
                ExtraHTTPHeaders = new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            }
            });
        }

        public async Task<T> DeserializeResponse<T>(IAPIResponse response)
        {
            var jsonString = await response.TextAsync();
            return JsonSerializer.Deserialize<T>(jsonString, JsonOptions);
        }

        public void AssertSuccessStatusCode(IAPIResponse response)
        {
            Assert.That((int)response.Status, Is.InRange(200, 299),
                $"Expected success status code but got {response.Status}");
        }
    }
}