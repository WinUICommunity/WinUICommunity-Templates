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

        public static string ReadGlobalUsingFileContent(string templatePath)
        {
            var globalUsingFilePath = GetGlobalUsingFilePath(templatePath);
            return File.ReadAllText(globalUsingFilePath);
        }

        public static void SaveGlobalUsingFileContent(string templatePath, string globalUsingFileContent)
        {
            var globalUsingFilePath = GetGlobalUsingFilePath(templatePath);
            File.WriteAllText(globalUsingFilePath, globalUsingFileContent);
        }
    }
}
