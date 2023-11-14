using System.Text.RegularExpressions;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeGlobalUsingFile
    {
        public NormalizeGlobalUsingFile(bool useJsonSettings, bool fileLogger, bool debugLogger, string templatePath)
        {
            string globalUsingFileContent = WizardHelper.ReadGlobalUsingFileContent(templatePath);

            if (string.IsNullOrEmpty(globalUsingFileContent))
            {
                return;
            }

            string patternAppHelper = @"global using static .*?Common\.AppHelper;";
            string patternLoggerSetup = @"global using static .*?Common\.LoggerSetup;";

            if (!useJsonSettings)
            {
                globalUsingFileContent = Regex.Replace(globalUsingFileContent, patternAppHelper, "");
            }

            if (!fileLogger && !debugLogger)
            {
                globalUsingFileContent = Regex.Replace(globalUsingFileContent, patternLoggerSetup, "");
            }

            WizardHelper.FormatDocument(WizardHelper.GetGlobalUsingFilePath(templatePath));
            WizardHelper.SaveGlobalUsingFileContent(templatePath, globalUsingFileContent);
        }
    }
}
