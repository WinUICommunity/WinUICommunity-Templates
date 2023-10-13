using System.IO;

using EnvDTE;
using EnvDTE80;

namespace WinUICommunity_VS_Templates.Options
{
    public static class WizardHelper
    {
        public static void FormatDocument(string filePath)
        {
            DTE2 dte = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            dte.ItemOperations.OpenFile(filePath);
            Document activeDoc = dte.ActiveDocument;

            TextSelection textSelection = activeDoc.Selection as TextSelection;
            textSelection.SelectAll();

            dte.ExecuteCommand("Edit.FormatDocument");
            activeDoc.Save();
            activeDoc.Close(vsSaveChanges.vsSaveChangesYes);
        }

        public static string GetAppConfigPath(string templatePath)
        {
            return Path.Combine(templatePath, "Common", "AppConfig.cs");
        }

        public static string GetAppUpdatePagePath(string templatePath)
        {
            return Path.Combine(templatePath, "Views", "Settings", "AppUpdateSettingPage.xaml.cs");
        }

        public static string GetAppUpdateViewModelPath(string templatePath)
        {
            return Path.Combine(templatePath, "ViewModels", "Settings", "AppUpdateSettingViewModel.cs");
        }

        public static string GetAppFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "App.xaml.cs");
        }

        public static string GetSettingPageFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "Views", "SettingsPage.xaml");
        }

        public static string GetGlobalUsingFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "GlobalUsings.cs");
        }

        public static string GetGeneralSettingFilePath(string templatePath)
        {
            return Path.Combine(templatePath, "Views", "Settings", "GeneralSettingPage.xaml");
        }

        public static string ReadAppConfigContent(string templatePath)
        {
            var filePath = GetAppConfigPath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveAppConfigContent(string templatePath, string appConfigContent)
        {
            var filePath = GetAppConfigPath(templatePath);
            File.WriteAllText(filePath, appConfigContent);
        }

        public static string ReadAppUpdatePageContent(string templatePath)
        {
            var filePath = GetAppUpdatePagePath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveAppUpdatePageContent(string templatePath, string appUpdatePageContent)
        {
            var filePath = GetAppUpdatePagePath(templatePath);
            File.WriteAllText(filePath, appUpdatePageContent);
        }

        public static string ReadAppUpdateViewModelContent(string templatePath)
        {
            var filePath = GetAppUpdateViewModelPath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveAppUpdateViewModelContent(string templatePath, string appUpdateViewModelContent)
        {
            var filePath = GetAppUpdateViewModelPath(templatePath);
            File.WriteAllText(filePath, appUpdateViewModelContent);
        }

        public static string ReadAppFileContent(string templatePath)
        {
            var filePath = GetAppFilePath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveAppFileContent(string templatePath, string appFileContent)
        {
            var filePath = GetAppFilePath(templatePath);
            File.WriteAllText(filePath, appFileContent);
        }

        public static string ReadSettingPageFileContent(string templatePath)
        {
            var filePath = GetSettingPageFilePath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveSettingPageFileContent(string templatePath, string settingsPageFileContent)
        {
            var filePath = GetSettingPageFilePath(templatePath);
            File.WriteAllText(filePath, settingsPageFileContent);
        }

        public static string ReadGlobalUsingFileContent(string templatePath)
        {
            var filePath = GetGlobalUsingFilePath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveGlobalUsingFileContent(string templatePath, string globalUsingFileContent)
        {
            var filePath = GetGlobalUsingFilePath(templatePath);
            File.WriteAllText(filePath, globalUsingFileContent);
        }

        public static string ReadGeneralSettingFileContent(string templatePath)
        {
            var filePath = GetGeneralSettingFilePath(templatePath);
            return File.ReadAllText(filePath);
        }

        public static void SaveGeneralSettingFileContent(string templatePath, string generalSettingFileContent)
        {
            var filePath = GetGeneralSettingFilePath(templatePath);
            File.WriteAllText(filePath, generalSettingFileContent);
        }
    }
}
