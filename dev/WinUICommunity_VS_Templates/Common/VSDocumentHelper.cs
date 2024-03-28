using System.IO;
using System.Xml;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace WinUICommunity_VS_Templates.Options
{
    public static class VSDocumentHelper
    {
        //public static string GetFilePath(string templatePath)
        //{
        //    return Path.Combine(templatePath, "Views", "Settings", "GeneralSettingPage.xaml");
        //}
        public static async void FormatDocument(DTE dte, string filePath, ProjectItem projectItem = null)
        {
            try
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
            catch (System.Exception)
            {
                var window = projectItem.Open();
                TextDocument textDocument = window.Document.Object() as TextDocument;
                textDocument.Selection.SelectAll();
                textDocument.Selection.SmartFormat();
                window.ProjectItem.Save();
                window.Close(vsSaveChanges.vsSaveChangesYes);
            }
        }

        public static void FormatXmlBasedFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                // Set the formatting options
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ", // Set this to your preferred indentation
                    NewLineOnAttributes = false
                };

                // Save the document with the new formatting
                using (var writer = XmlWriter.Create(filePath, settings))
                {
                    xmlDoc.Save(writer);
                }
            }
        }
    }
}
