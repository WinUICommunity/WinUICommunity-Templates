using System.Text.RegularExpressions;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeGeneralSettingFile
    {
        public NormalizeGeneralSettingFile(bool useJsonSettings, bool useSettingsPage, bool useDeveloperMode, bool useGeneralSettingPage, string templatePath)
        {
            if (useSettingsPage && useGeneralSettingPage)
            {
                if (!useDeveloperMode || !useJsonSettings)
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
