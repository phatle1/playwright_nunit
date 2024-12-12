using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Playwright;

namespace PortalTalk.AutomationTest.BackendHelper.APILayer
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Playwright;

    public class APIConnection
    {
        public async Task<string> GetPTAccessToken(string scope = "https://test.portaltalk.net/.default")
        {
            if (string.IsNullOrEmpty(scope))
            {
                throw new ArgumentNullException(nameof(scope), "Scope cannot be null or empty");
            }
            string TenantId = "2cd51ada-83c6-489c-ac43-a932277a4dfd";
            string ClientId = "c66c39a8-cfa7-4799-bb89-d9851c2fa619";
            string ClientSecret = "1bMxMur7rEW33CwV8u5b3eACufHGy06mVjPsQQzklz4=";
            var authority = $"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token";
            var oauth2Request = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "scope", scope }
            };

            try
            {
                using var client = new HttpClient();
                var content = new FormUrlEncodedContent(oauth2Request);
                var response = await client.PostAsync(authority, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                if (jsonResponse.TryGetProperty("access_token", out JsonElement tokenElement))
                {
                    string? token = tokenElement.GetString();
                    if (!string.IsNullOrEmpty(token))
                    {
                        return token;
                    }                  
                }
                throw new InvalidOperationException("Response did not contain an access_token property");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Failed to retrieve access token: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to parse response: {ex.Message}", ex);
            }
        }
    }
}