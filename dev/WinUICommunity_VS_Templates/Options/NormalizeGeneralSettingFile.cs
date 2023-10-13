using System.Text.RegularExpressions;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeGeneralSettingFile
    {
        public NormalizeGeneralSettingFile(bool useJsonSettings, bool useSettingsPage, bool useDeveloperMode, bool useGeneralSettingPage, string templatePath)
        {
            if (useSettingsPage && useDeveloperMode && useGeneralSettingPage)
            {
                string generalSettingFileContent = WizardHelper.ReadGeneralSettingFileContent(templatePath);
                string pattern = @"xmlns:local=""using:\w+\.Common""";

                if (!useJsonSettings)
                {
                    generalSettingFileContent = Regex.Replace(generalSettingFileContent, pattern, "");
                }

                WizardHelper.FormatDocument(WizardHelper.GetGeneralSettingFilePath(templatePath));
                WizardHelper.SaveGeneralSettingFileContent(templatePath, generalSettingFileContent);
            }
        }
    }
}
