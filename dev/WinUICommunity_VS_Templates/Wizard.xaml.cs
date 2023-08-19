using System.Windows;

namespace WinUICommunity_VS_Templates
{
    public partial class Wizard
    {
        public static bool AddJsonSettings;
        public static bool AddDynamicLocalization;
        public static bool AddEditorConfig;
        public static bool AddSolutionFolder;
        public static bool AddHomeLandingPage;
        public static bool AddSettingsPage;
        public static bool AddThemeSettingPage;
        public static bool AddAppUpdatePage;
        public static bool AddAboutPage;
        public Wizard()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            AddJsonSettings = tgJsonSettings.IsOn;
            AddDynamicLocalization = tgDynamicLocalization.IsOn;
            AddEditorConfig = tgEditorConfig.IsOn;
            AddSolutionFolder = tgSolutionFolder.IsOn;
            AddHomeLandingPage = tgHomePage.IsOn;
            AddSettingsPage = tgSettingsPage.IsOn;
            AddThemeSettingPage = tgThemeSetting.IsOn;
            AddAppUpdatePage = tgAppUpdate.IsOn;
            AddAboutPage = tgAboutSetting.IsOn;
            this.Close();
        }
    }
}
