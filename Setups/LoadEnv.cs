namespace PortalTalk.AutomationTest.Setups
{
    using System;
    using System.IO;
    public static class LoadEnv
    {
        public static void Load(string env)
        {
            try
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName ?? "";
                projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName ?? "";
                var env_path = $"{projectDirectory}/.env.{env}";
                if (!File.Exists(env_path))
                {
                    return;
                }
                foreach (var line in File.ReadAllLines(env_path))
                {
                    var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        continue;
                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}