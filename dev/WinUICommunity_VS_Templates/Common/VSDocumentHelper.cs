using System.IO;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace WinUICommunity_VS_Templates.Options
{
    public static class VSDocumentHelper
    {
        //public static string GetFilePath(string templatePath)
        //{
        //    return Path.Combine(templatePath, "Views", "Settings", "GeneralSettingPage.xaml");
        //}
        public static async void FormatDocument(DTE dte, string filePath)
        {
            if (File.Exists(filePath))
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                dte.ItemOperations.OpenFile(filePath);
                Document activeDoc = dte.ActiveDocument;

                TextSelection textSelection = activeDoc.Selection as TextSelection;
                textSelection.SelectAll();

                dte.ExecuteCommand("Edit.FormatDocument");
                activeDoc.Save();
                activeDoc.Close(vsSaveChanges.vsSaveChangesYes);
            }
        }
    }
}
