using System.Windows;
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
            WizardConfig.AddHomeLandingPage = tgHomePage.IsOn;
        }

        private void tgSettingsPage_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddSettingsPage = tgSettingsPage.IsOn;
        }

        private void tgGeneralSettingPage_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddGeneralSettingPage = tgGeneralSettingPage.IsOn;
        }

        private void tgDeveloperMode_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddDeveloperModeSetting = tgDeveloperMode.IsOn;
        }

        private void tgThemeSetting_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddThemeSettingPage = tgThemeSetting.IsOn;
        }

        private void tgAppUpdate_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddAppUpdatePage = tgAppUpdate.IsOn;
        }

        private void tgAboutSetting_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddAboutPage = tgAboutSetting.IsOn;
        }
    }
}
