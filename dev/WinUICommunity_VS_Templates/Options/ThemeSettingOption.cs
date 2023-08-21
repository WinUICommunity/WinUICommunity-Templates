using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class ThemeSettingOption
    {
        string baseThemeSettingCode = """<!--  THEMESETTING  -->""";
        string themeSettingCode =
"""
<wuc:SettingsCard x:Name="ThemeSetting"
                              Click="OnSettingCard_Click"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" />
""";
        string themeSettingCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="ThemeSetting"
                              Click="OnSettingCard_Click"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" /> -->
""";
        string themeSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="ThemeSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=ThemeSetting}"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" />
""";
        string themeSettingMVVMCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="ThemeSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=ThemeSetting}"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" /> -->
""";
        public ThemeSettingOption(Wizard wizard, bool isMVVMTemplate, string templatePath)
        {
            if (wizard.AddSettingsPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                string settingsPageFileContent = WizardHelper.ReadSettingPageFileContent(templatePath);

                if (wizard.AddThemeSettingPage)
                {
                    if (isMVVMTemplate)
                    {
                        string SERVICE_KEY = "//SERVICE";

                        // Add Service
                        string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<ThemeSettingViewModel>();";
                        appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);

                        settingsPageFileContent = settingsPageFileContent.Replace(baseThemeSettingCode, themeSettingMVVMCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseThemeSettingCode, themeSettingCode);
                    }
                }
                else
                {
                    if (isMVVMTemplate)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseThemeSettingCode, themeSettingMVVMCommentCode);
                    }
                    else
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(baseThemeSettingCode, themeSettingCommentCode);
                    }
                }

                WizardHelper.SaveSettingPageFileContent(templatePath, settingsPageFileContent);
                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
