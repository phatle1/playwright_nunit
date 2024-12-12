
using System.Text.Json;
using Microsoft.Playwright;

namespace PortalTalk.AutomationTest.Utils
{
    public static class ResponseFormatter
    {
        private static JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true // This makes the JSON output formatted/pretty
        };

        public static async Task<T> GetFormattedResponse<T>(IAPIResponse response)
        {
            try
            {
                var jsonString = await response.JsonAsync<JsonElement>();
                var formattedJson = JsonSerializer.Serialize(jsonString, JsonOptions);
                return JsonSerializer.Deserialize<T>(formattedJson, JsonOptions);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to format response: {ex.Message}");
            }
        }

        public static async Task<string> GetFormattedJsonString(IAPIResponse response)
        {
            var jsonElement = await response.JsonAsync<JsonElement>();
            return JsonSerializer.Serialize(jsonElement, JsonOptions);
        }

    }

}
