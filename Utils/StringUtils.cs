
using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
namespace PortalTalk.AutomationTest.Utils
{
    public class StringUtils
    {
        public static string IterateDictionary(string stringAsJson)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringAsJson);
            StringBuilder result = new();
            foreach (var item in dictionary)
            {
                result.Append($"{item.Key}: {item.Value}\n");
            }
            return result.ToString();
        }
       
    }
}