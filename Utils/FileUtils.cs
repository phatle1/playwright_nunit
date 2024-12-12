
using System.Threading.Tasks;
using Microsoft.Playwright;
using PortalTalk.AutomationTest.Pages;
namespace PortalTalk.AutomationTest.Utils
{

    public static class FileUtils
    {
        public static void UpdateAccessTokenFromENV(string env, string value)
        {

            string envPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", $".env.{env}"));
            string new_token = $"TOKEN={value}";

            if (File.Exists(envPath))
            {
                var lines = File.ReadAllLines(envPath);
                var tokenLineIndex = Array.FindIndex<string>(lines, line => line.StartsWith("TOKEN="));
                if (tokenLineIndex >= 0)
                {
                    lines[tokenLineIndex] = new_token;
                    File.WriteAllLines(envPath, lines);
                }
                else
                {
                    File.AppendAllText(envPath, Environment.NewLine + new_token);
                }
            }
        }

    }
}