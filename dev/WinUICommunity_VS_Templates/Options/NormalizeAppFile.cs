using System.Text.RegularExpressions;

using EnvDTE;

using EnvDTE80;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeAppFile
    {
        public NormalizeAppFile(string templatePath)
        {
            string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

            string pattern = @"(\r\n|\r|\n){3,}";

            appFileContent = appFileContent.Replace("//JSONCONFIG", "");
            appFileContent = appFileContent.Replace("//JSONCONFIGMVVM", "");
            appFileContent = appFileContent.Replace("//SERVICE", "");

            appFileContent = Regex.Replace(appFileContent, pattern, "\n\n");

            WizardHelper.SaveAppFileContent(templatePath, appFileContent);

            WizardHelper.FormatDocument(WizardHelper.GetAppFilePath(templatePath));
        }
    }
}
