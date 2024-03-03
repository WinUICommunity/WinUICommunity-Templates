﻿using System.Windows;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class PagesPages : Page
    {
        public PagesPages()
        {
            InitializeComponent();
        }

        private void tgHomePage_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseHomeLandingPage = tgHomePage.IsOn;
        }

        private void tgSettingsPage_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseSettingsPage = tgSettingsPage.IsOn;
        }

        private void tgGeneralSettingPage_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseGeneralSettingPage = tgGeneralSettingPage.IsOn;
        }

        private void tgDeveloperMode_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseDeveloperModeSetting = tgDeveloperMode.IsOn;
        }

        private void tgThemeSetting_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseThemeSettingPage = tgThemeSetting.IsOn;
        }

        private void tgAppUpdate_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseAppUpdatePage = tgAppUpdate.IsOn;
        }

        private void tgAboutSetting_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.UseAboutPage = tgAboutSetting.IsOn;
        }
    }
}
