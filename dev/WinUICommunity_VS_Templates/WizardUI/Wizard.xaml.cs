using System.Windows;

namespace WinUICommunity_VS_Templates
{
    public partial class Wizard : HandyControl.Controls.Window
    {
        public bool AddJsonSettings;
        public bool AddDynamicLocalization;
        public bool AddEditorConfig;
        public bool AddSolutionFolder;
        public bool AddHomeLandingPage;
        public bool AddSettingsPage;
        public bool AddGeneralSettingPage;
        public bool AddThemeSettingPage;
        public bool AddAppUpdatePage;
        public bool AddAboutPage;

        public Wizard()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            AddJsonSettings = tgJsonSettings.IsOn;
            AddDynamicLocalization = tgDynamicLocalization.IsOn;
            AddEditorConfig = tgEditorConfig.IsOn;
            AddSolutionFolder = tgSolutionFolder.IsOn;
            AddHomeLandingPage = tgHomePage.IsOn;
            AddSettingsPage = tgSettingsPage.IsOn;
            AddGeneralSettingPage = tgGeneralSettingPage.IsOn;
            AddThemeSettingPage = tgThemeSetting.IsOn;
            AddAppUpdatePage = tgAppUpdate.IsOn;
            AddAboutPage = tgAboutSetting.IsOn;

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult.HasValue && DialogResult.Value)
            {
            }
            else
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            DialogResult = false;
        }
    }
}
