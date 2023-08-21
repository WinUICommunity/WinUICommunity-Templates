using System.IO;

namespace WinUICommunity_VS_Templates.Options
{
    public static class WizardHelper
    {
        public static string GetAppFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "App.xaml.cs");
        }

        public static string GetSettingPageFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "Views", "SettingsPage.xaml");
        }

        public static string ReadAppFileContent(string templatePath)
        {
            var appFilePath = GetAppFilePath(templatePath);
            return File.ReadAllText(appFilePath);
        }

        public static void SaveAppFileContent(string templatePath, string appFileContent)
        {
            var appFilePath = GetAppFilePath(templatePath);
            File.WriteAllText(appFilePath, appFileContent);
        }

        public static string ReadSettingPageFileContent(string templatePath)
        {
            var settingsPageFilePath = GetSettingPageFilePath(templatePath);
            return File.ReadAllText(settingsPageFilePath);
        }

        public static void SaveSettingPageFileContent(string templatePath, string settingsPageFileContent)
        {
            var settingsPageFilePath = GetSettingPageFilePath(templatePath);
            File.WriteAllText(settingsPageFilePath, settingsPageFileContent);
        }
    }
}
