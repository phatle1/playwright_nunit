using DotNetEnv;

namespace PortalTalk.AutomationTest.Utils
{
    public static class EnvUtils
    {

        public static string BASE_URL => Environment.GetEnvironmentVariable("BASE_URL") ?? "";
        public static string USERNAME => Environment.GetEnvironmentVariable("USER_NAME") ?? "";
        public static string PWD => Environment.GetEnvironmentVariable("PWD") ?? "";
        public static string USERNAME1 => Environment.GetEnvironmentVariable("USER_NAME1") ?? "";
        public static string PWD1 => Environment.GetEnvironmentVariable("PWD1") ?? "";
         public static string USERNAME2 => Environment.GetEnvironmentVariable("USER_NAME2") ?? "";
        public static string PWD2 => Environment.GetEnvironmentVariable("PWD2") ?? "";
        public static string TOKEN => Environment.GetEnvironmentVariable("TOKEN") ?? "";
        public static string END_POINT => Environment.GetEnvironmentVariable("END_POINT") ?? "";
    }
}
