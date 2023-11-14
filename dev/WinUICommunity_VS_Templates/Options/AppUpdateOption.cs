namespace WinUICommunity_VS_Templates.Options
{
    public class AppUpdateOption
    {
        public AppUpdateOption(bool useSettingsPage, bool useAppUpdatePage, bool useJsonSettings, bool isMVVMTemplate, string templatePath)
        {
            if (useSettingsPage && useAppUpdatePage)
            {
                if (!useJsonSettings)
                {
                    if (isMVVMTemplate)
                    {
                        var appUpdateViewModelFileContent = WizardHelper.ReadAppUpdateViewModelContent(templatePath);
                        if (!string.IsNullOrEmpty(appUpdateViewModelFileContent))
                        {
                            appUpdateViewModelFileContent = appUpdateViewModelFileContent.Replace("LastUpdateCheck = Settings.LastUpdateCheck;", "\n//Todo:\n//LastUpdateCheck = Settings.LastUpdateCheck;");
                            appUpdateViewModelFileContent = appUpdateViewModelFileContent.Replace("Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();", "\n//Todo:\n//Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();");
                            WizardHelper.SaveAppUpdateViewModelContent(templatePath, appUpdateViewModelFileContent);
                            WizardHelper.FormatDocument(WizardHelper.GetAppUpdateViewModelPath(templatePath));
                        }
                    }
                    else
                    {
                        var appUpdatePageFileContent = WizardHelper.ReadAppUpdatePageContent(templatePath);
                        if (!string.IsNullOrEmpty(appUpdatePageFileContent))
                        {
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
}
