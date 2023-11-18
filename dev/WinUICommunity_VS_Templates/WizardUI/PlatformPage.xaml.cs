using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class PlatformPage : Page
    {
        public PlatformPage()
        {
            InitializeComponent();
        }

        private string GetRuntimeIdentifiers(string platforms)
        {
            string rid = "win10-x86;win10-x64;win10-arm64";
            if (!platforms.Contains("x86"))
            {
                rid = rid.Replace("win10-x86;", "");
            }

            if (!platforms.Contains("x64"))
            {
                rid = rid.Replace("win10-x64;", "");
            }

            if (!platforms.Contains("ARM64"))
            {
                rid = rid.Replace("win10-arm64", "");
            }

            if (rid.EndsWith(";"))
            {
                var lastIndex = rid.LastIndexOf(";");
                rid = rid.Remove(lastIndex);
            }

            return rid.Trim();
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            string platforms = "x86;x64;ARM64";
            if (chk.IsChecked.Value)
            {
                platforms = string.Empty;
                platforms = platforms + chk.Tag + ";";

                if (platforms.EndsWith(";"))
                {
                    var lastIndex = platforms.LastIndexOf(";");
                    platforms = platforms.Remove(lastIndex);
                }
            }
            WizardConfig.Platforms = platforms;
            WizardConfig.RuntimeIdentifiers = GetRuntimeIdentifiers(platforms);
        }

        private void cmbNetVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WizardConfig.DotNetVersion = (cmbNetVersion.SelectedItem as ComboBoxItem).Tag.ToString();
        }

        private void tgAccelerateBuilds_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.AddAccelerateBuilds = tgAccelerateBuilds.IsOn;
        }

        private void tgJsonSettings_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.AddJsonSettings = tgJsonSettings.IsOn;
        }

        private void tgDynamicLocalization_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.AddDynamicLocalization = tgDynamicLocalization.IsOn;
        }

        private void tgEditorConfig_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.AddEditorConfig = tgEditorConfig.IsOn;
        }

        private void tgSolutionFolder_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.AddSolutionFolder = tgSolutionFolder.IsOn;
        }
    }
}
