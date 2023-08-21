using System.Text.RegularExpressions;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeGlobalUsingFile
    {
        public NormalizeGlobalUsingFile(Wizard wizard, string templatePath)
        {
            if (!wizard.AddJsonSettings)
            {
                string globalUsingFileContent = WizardHelper.ReadGlobalUsingFileContent(templatePath);
                string pattern = @"global using static .*?Common\.AppHelper;";
                string modifiedText = Regex.Replace(globalUsingFileContent, pattern, "");
                WizardHelper.SaveGlobalUsingFileContent(templatePath, modifiedText);
                WizardHelper.FormatDocument(WizardHelper.GetGlobalUsingFilePath(templatePath));
            }
        }
    }
}
