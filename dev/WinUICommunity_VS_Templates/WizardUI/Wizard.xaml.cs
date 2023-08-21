using System.Windows;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.Shell.Interop;

namespace WinUICommunity_VS_Templates
{
    public partial class Wizard
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
            
            DTE2 dte = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox).Close();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DTE2 dte = (DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox).Close();
            dte.ExecuteCommand("Build.Cancel");
            Close();
        }
    }
}
