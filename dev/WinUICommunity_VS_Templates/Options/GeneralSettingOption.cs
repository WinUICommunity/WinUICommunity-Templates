using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class GeneralSettingOption
    {
        string baseGeneralSettingCode = "<!--  GENERALSETTING  -->";
        string generalSettingCode =
"""
<wuc:SettingsCard x:Name="GeneralSetting"
                              Click="OnSettingCard_Click"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" />
""";
        string generalSettingCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="GeneralSetting"
                              Click="OnSettingCard_Click"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" /> -->
""";
        string generalSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="GeneralSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=GeneralSetting}"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" />
""";
        string generalSettingMVVMCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="GeneralSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=GeneralSetting}"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" /> -->
""";
        public GeneralSettingOption(Wizard wizard, bool isMVVMTemplate, string templatePath)
        {
            if (wizard.AddSettingsPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                string settingsPageFileContent = WizardHelper.ReadSettingPageFileContent(templatePath);

                if (wizard.AddGeneralSettingPage)
                {
                    if (isMVVMTemplate)
                    {
                        string SERVICE_KEY = "//SERVICE";

                        // Add Service
                        string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<GeneralSettingViewModel>();";
                        appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);
                        settingsPageFileContent = settingsPageFileContent.Replace(baseGeneralSettingCode, generalSettingMVVMCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseGeneralSettingCode, generalSettingCode);
                    }
                }
                else
                {
                    if (isMVVMTemplate)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseGeneralSettingCode, generalSettingMVVMCommentCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseGeneralSettingCode, generalSettingCommentCode);
                    }
                }

                WizardHelper.SaveSettingPageFileContent(templatePath, settingsPageFileContent);
                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
