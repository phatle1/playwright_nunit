
using System;
using System.Security.Cryptography;
using System.Text;
namespace PortalTalk.AutomationTest.Utils
{
    public class RandomGenerator
    {
        public static string GetRandomNumberWithSpecificDigits(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Number of digits must be greater than zero.");
            }
            Random random = new();
            int minValue = (int)Math.Pow(10, length - 1);
            int maxValue = (int)Math.Pow(10, length) - 1;
            return random.Next(minValue, maxValue + 1).ToString();
        }

        public static string GetRandomString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[length];
            RandomNumberGenerator.Fill(data);

            StringBuilder result = new(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }

    }
}