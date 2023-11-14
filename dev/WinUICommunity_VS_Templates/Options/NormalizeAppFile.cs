using System.Text.RegularExpressions;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeAppFile
    {
        public NormalizeAppFile(string templatePath)
        {
            string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

            if (string.IsNullOrEmpty(appFileContent))
            {
                return;
            }

            if (appFileContent.Contains("private static string StringsFolderPath { get; set; } = string.Empty;"))
            {
                if (appFileContent.Contains("public static T GetService<T>() where T : class"))
                {
                    appFileContent = appFileContent.Replace("public static T GetService<T>() where T : class", "\n    public static T GetService<T>() where T : class");
                }
                else
                {
                    appFileContent = appFileContent.Replace("public App()", "\n    public App()");
                }
            }

            if (!appFileContent.Contains("JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));") && !appFileContent.Contains("JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));"))
            {
                string jsonNavigationViewServicePattern = @"JsonNavigationViewService = new JsonNavigationViewService\(\);(\s*\n\s*)}";
                appFileContent = Regex.Replace(appFileContent, jsonNavigationViewServicePattern, "JsonNavigationViewService = new JsonNavigationViewService(); \n    }");
            }

            string nameSpacePattern = @"namespace\s+(\w+);";
            Match match = Regex.Match(appFileContent, nameSpacePattern);
            if (match.Success)
            {
                string namespaceName = match.Value;
                appFileContent = appFileContent.Replace($"using Windows.Storage;\r\n{namespaceName}", $"using Windows.Storage;\n\n{namespaceName}");
            }

            string blanklinePattern = @"}\s*\n\s*}";
            appFileContent = Regex.Replace(appFileContent, blanklinePattern, "}\n}");

            string pattern = @"(\r\n|\r|\n){3,}";
            appFileContent = Regex.Replace(appFileContent, pattern, "\n\n");

            string blanklinePattern2 = @"^\s*\n\s*(?=namespace)";
            appFileContent = Regex.Replace(appFileContent, blanklinePattern2, "");

            string blanklinePattern3 = @"(\n\s*){3,}";
            appFileContent = Regex.Replace(appFileContent, blanklinePattern3, "\n\n");

            WizardHelper.SaveAppFileContent(templatePath, appFileContent);

            WizardHelper.FormatDocument(WizardHelper.GetAppFilePath(templatePath));
        }
    }
}
