using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class AppUpdateOption
    {
        string baseAppUpdateSettingCode = """<!--  APPUPDATESETTING  -->""";
        string appUpdateSettingCode =
"""
<wuc:SettingsCard x:Name="AppUpdateSetting"
                              Click="OnSettingCard_Click"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" />
""";
        string appUpdateSettingCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="AppUpdateSetting"
                              Click="OnSettingCard_Click"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" /> -->
""";
        string appUpdateSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="AppUpdateSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AppUpdateSetting}"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" />
""";
        string appUpdateSettingMVVMCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="AppUpdateSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AppUpdateSetting}"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" /> -->
""";
        public AppUpdateOption(bool useSettingsPage, bool useAppUpdatePage, bool useJsonSettings, bool isMVVMTemplate, string templatePath)
        {
            if (useSettingsPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                string settingsPageFileContent = WizardHelper.ReadSettingPageFileContent(templatePath);

                if (useAppUpdatePage)
                {
                    if (isMVVMTemplate)
                    {
                        string SERVICE_KEY = "//SERVICE";

                        // Add Service
                        string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<AppUpdateSettingViewModel>();";
                        appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);

                        settingsPageFileContent = settingsPageFileContent.Replace(baseAppUpdateSettingCode, appUpdateSettingMVVMCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAppUpdateSettingCode, appUpdateSettingCode);
                    }
                    
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
                else
                {
                    if (isMVVMTemplate)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAppUpdateSettingCode, appUpdateSettingMVVMCommentCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAppUpdateSettingCode, appUpdateSettingCommentCode);
                    }
                }

                WizardHelper.SaveSettingPageFileContent(templatePath, settingsPageFileContent);
                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
