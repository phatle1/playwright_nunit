using System.Globalization;

namespace PortalTalk.AutomationTest.Utils
{
    public static class DateTimeUtils
    {
        private static readonly string[] DateFormats =
        [
            "MM/dd/yyyy, hh:mm tt",
            "M/d/yyyy, hh:mm tt",
            "MM/dd/yyyy, h:mm tt",
            "M/d/yyyy, h:mm tt",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm",
            "dd/MM/yyyy HH:mm",
            "MM/dd/yyyy HH:mm",
            "dd-MM-yyyy HH:mm",
            "MM-dd-yyyy HH:mm"
        ];

        public static string GetCurrentDateTime(string Format)
        {
            return DateTime.Now.ToString(Format);
        }

        public static DateTime StringToDateTime(string time_as_string)
        {
            try
            {
                var time_as_datetime = DateTime.ParseExact(time_as_string, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                return time_as_datetime;
            }
            catch (FormatException)
            {
                throw new FormatException($"Invalid datetime format. Supported formats: {string.Join(", ", DateFormats)}");
            }
        }
    }
}