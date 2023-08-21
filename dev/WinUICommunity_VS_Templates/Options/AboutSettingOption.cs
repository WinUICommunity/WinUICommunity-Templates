using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class AboutSettingOption
    {
        string baseAboutSettingCode = """<!--  ABOUTSETTING  -->""";
        string aboutSettingCode =
"""
<wuc:SettingsCard x:Name="AboutSetting"
                              Click="OnSettingCard_Click"
                              Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" />
""";
        string aboutSettingCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="AboutSetting"
                              Click="OnSettingCard_Click"
                              Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" /> -->
""";
        string aboutSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="AboutSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AboutSetting}"
                              Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" />
""";
        string aboutSettingMVVMCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="AboutSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AboutSetting}"
                              Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" /> -->
""";
        public AboutSettingOption(bool useSettingsPage, bool useAboutPage, bool isMVVMTemplate, string templatePath)
        {
            if (useSettingsPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                string settingsPageFileContent = WizardHelper.ReadSettingPageFileContent(templatePath);

                if (useAboutPage)
                {
                    if (isMVVMTemplate)
                    {
                        string SERVICE_KEY = "//SERVICE";

                        // Add Service
                        string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<AboutUsSettingViewModel>();";
                        appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);

                        settingsPageFileContent = settingsPageFileContent.Replace(baseAboutSettingCode, aboutSettingMVVMCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAboutSettingCode, aboutSettingCode);
                    }
                }
                else
                {
                    if (isMVVMTemplate)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAboutSettingCode, aboutSettingMVVMCommentCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseAboutSettingCode, aboutSettingCommentCode);
                    }
                }

                WizardHelper.SaveSettingPageFileContent(templatePath, settingsPageFileContent);
                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
