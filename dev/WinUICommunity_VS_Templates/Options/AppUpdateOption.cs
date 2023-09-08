namespace WinUICommunity_VS_Templates.Options
{
    public class AppUpdateOption
    {
        public AppUpdateOption(bool useSettingsPage, bool useAppUpdatePage, bool useJsonSettings, bool isMVVMTemplate, string templatePath)
        {
            if (useSettingsPage && useAppUpdatePage)
            {
                if (useJsonSettings)
                {
                    string appConfigFileContent = WizardHelper.ReadAppConfigContent(templatePath);
                    var lastUpdateCheckProperty = "public virtual string LastUpdateCheck { get; set; }";
                    appConfigFileContent = appConfigFileContent.Replace("// Docs: https://github.com/Nucs/JsonSettings", $"// Docs: https://github.com/Nucs/JsonSettings\n\n    {lastUpdateCheckProperty}");
                    WizardHelper.SaveAppConfigContent(templatePath, appConfigFileContent);
                }
                else
                {
                    if (isMVVMTemplate)
                    {
                        var appUpdateViewModelFileContent = WizardHelper.ReadAppUpdateViewModelContent(templatePath);
                        appUpdateViewModelFileContent = appUpdateViewModelFileContent.Replace("LastUpdateCheck = Settings.LastUpdateCheck;", "\n//Todo:\n//LastUpdateCheck = Settings.LastUpdateCheck;");
                        appUpdateViewModelFileContent = appUpdateViewModelFileContent.Replace("Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();", "\n//Todo:\n//Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();");
                        WizardHelper.SaveAppUpdateViewModelContent(templatePath, appUpdateViewModelFileContent);
                        WizardHelper.FormatDocument(WizardHelper.GetAppUpdateViewModelPath(templatePath));
                    }
                    else
                    {
                        var appUpdatePageFileContent = WizardHelper.ReadAppUpdatePageContent(templatePath);
                        appUpdatePageFileContent = appUpdatePageFileContent.Replace("TxtLastUpdateCheck.Text = Settings.LastUpdateCheck;", "//Todo:\n//TxtLastUpdateCheck.Text = Settings.LastUpdateCheck;");
                        appUpdatePageFileContent = appUpdatePageFileContent.Replace("Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();", "\n//Todo:\n//Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();");
                        WizardHelper.SaveAppUpdatePageContent(templatePath, appUpdatePageFileContent);
                        WizardHelper.FormatDocument(WizardHelper.GetAppUpdatePagePath(templatePath));
                    }
                }
            }
        }
    }
}
