using System.IO;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace WinUICommunity_VS_Templates.Options
{
    public static class WizardHelper
    {
        //public static string GetFilePath(string templatePath)
        //{
        //    return Path.Combine(templatePath, "Views", "Settings", "GeneralSettingPage.xaml");
        //}
        public static async void FormatDocument(string filePath)
        {
            if (File.Exists(filePath))
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                DTE2 dte = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
                dte.ItemOperations.OpenFile(filePath);
                Document activeDoc = dte.ActiveDocument;

                TextSelection textSelection = activeDoc.Selection as TextSelection;
                textSelection.SelectAll();

                dte.ExecuteCommand("Edit.FormatDocument");
                activeDoc.Save();
                activeDoc.Close(vsSaveChanges.vsSaveChangesYes);
            }
        }

        public static string GetAppUpdatePagePath(string templatePath)
        {
            return Path.Combine(templatePath, "Views", "Settings", "AppUpdateSettingPage.xaml.cs");
        }

        public static string GetAppUpdateViewModelPath(string templatePath)
        {
            return Path.Combine(templatePath, "ViewModels", "Settings", "AppUpdateSettingViewModel.cs");
        }

        public static string ReadAppUpdatePageContent(string templatePath)
        {
            var filePath = GetAppUpdatePagePath(templatePath);
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }

        public static void SaveAppUpdatePageContent(string templatePath, string appUpdatePageContent)
        {
            var filePath = GetAppUpdatePagePath(templatePath);
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                File.WriteAllText(filePath, appUpdatePageContent);
            }
        }

        public static string ReadAppUpdateViewModelContent(string templatePath)
        {
            var filePath = GetAppUpdateViewModelPath(templatePath);
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }

        public static void SaveAppUpdateViewModelContent(string templatePath, string appUpdateViewModelContent)
        {
            var filePath = GetAppUpdateViewModelPath(templatePath);
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                File.WriteAllText(filePath, appUpdateViewModelContent);
            }
        }
    }
}
