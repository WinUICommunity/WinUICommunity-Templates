using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HandyControl.Controls;

using WinUICommunity_VS_Templates.Shell;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class Wizard : HandyControl.Controls.Window
    {
        public Dictionary<string, PackageRefrence> LibraryDic;

        public string DotNetVersion;
        public string Platforms;
        public string RuntimeIdentifiers;
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
        public bool AddAccelerateBuilds;

        public Wizard()
        {
            InitializeComponent();
            LibraryDic = new();

            CreateBoxes(PreDefinedLibrary.InitWinUICommunity(), WinUICommunityPanel);
            CreateBoxes(PreDefinedLibrary.InitUseful(), GeneralPanel);
            CreateBoxes(PreDefinedLibrary.InitEFCore(), EFCorePanel);
            CreateBoxes(PreDefinedLibrary.InitCommunityToolkit(), CommunityToolkitPanel);
            CreateBoxes(PreDefinedLibrary.InitMVVM(), MVVMPanel);
            CreateBoxes(PreDefinedLibrary.InitLog(), LogPanel);
        }

        public void CreateBoxes(List<Library> libraries, Panel panel)
        {
            foreach (var lib in libraries)
            {
                var option = new LibraryOptionUC
                {
                    Title = $"{lib.Name} - {lib.Version}"
                };

                option.Checked += (s, e) =>
                {
                    LibraryDic.Add(lib.Name, new PackageRefrence($"""<PackageReference Include="{lib.Name}" Version="{lib.Version}" />""", lib.CheckBeforeInsert));
                };

                option.Unchecked += (s, e) =>
                {
                    LibraryDic.Remove(lib.Name);
                };

                panel.Children.Add(option);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            DotNetVersion = (cmbNetVersion.SelectedItem as ComboBoxItem).Tag.ToString();
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
            AddAccelerateBuilds = tgAccelerateBuilds.IsOn;

            Platforms = GetPlatforms();
            RuntimeIdentifiers = GetRuntimeIdentifiers(Platforms);

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

        private string GetPlatforms()
        {
            string platforms = "x86;x64;ARM64";
            if (cmbPlatforms.SelectedItems.Count > 0)
            {
                platforms = string.Empty;
                foreach (CheckComboBoxItem item in cmbPlatforms.SelectedItems)
                {
                    platforms = platforms + item.Tag + ";";
                }

                if (platforms.EndsWith(";"))
                {
                    var lastIndex = platforms.LastIndexOf(";");
                    platforms = platforms.Remove(lastIndex);
                }
            }

            return platforms;
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
    }
}
