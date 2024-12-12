namespace PortalTalk.AutomationTest.Setups
{
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using System;
    public class PlaywrightLogger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class LogTestExecutionAttribute : Attribute, ITestAction
    {

        public void BeforeTest(ITest test)
        {
            PlaywrightLogger.Log($"Starting test: {test.Name}");
        }

        public void AfterTest(ITest test)
        {
            PlaywrightLogger.Log($"Finished test: {test.Name}");
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}