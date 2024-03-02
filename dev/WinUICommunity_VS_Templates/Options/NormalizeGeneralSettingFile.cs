using System.Text.RegularExpressions;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeGeneralSettingFile
    {
        public NormalizeGeneralSettingFile(string templatePath)
        {
            if (WizardConfig.UseSettingsPage && WizardConfig.UseGeneralSettingPage)
            {
                if (!WizardConfig.UseDeveloperModeSetting || !WizardConfig.UseJsonSettings)
                {
                    string generalSettingFileContent = WizardHelper.ReadGeneralSettingFileContent(templatePath);
                    if (string.IsNullOrEmpty(generalSettingFileContent))
                    {
                        return;
                    }
                    string pattern = @"xmlns:local=""using:.*?\.Common""";

                    generalSettingFileContent = Regex.Replace(generalSettingFileContent, pattern, "");

                    WizardHelper.FormatDocument(WizardHelper.GetGeneralSettingFilePath(templatePath));
                    WizardHelper.SaveGeneralSettingFileContent(templatePath, generalSettingFileContent);
                }
            }
        }
    }
}
