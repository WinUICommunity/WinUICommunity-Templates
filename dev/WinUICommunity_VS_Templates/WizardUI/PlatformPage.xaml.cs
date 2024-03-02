﻿using System.Collections.Generic;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class PlatformPage : Page
    {
        private List<string> platformList = new List<string> { "x86", "x64", "ARM64" };
        internal static PlatformPage Instance { get; private set; }
        public PlatformPage()
        {
            InitializeComponent();
            Instance = this;
            Loaded += PlatformPage_Loaded;
        }

        private void PlatformPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (WizardConfig.IsBlank)
            {
                if (tgDynamicLocalization != null && tgJsonSettings != null)
                {
                    tgDynamicLocalization.IsEnabled = false;
                    tgJsonSettings.IsEnabled = false;
                }
            }
        }
        public void UpdateCheckBoxs()
        {
            if (chk64 != null)
            {
                if (chk64.IsChecked == true)
                {
                    chk64.IsChecked = false;
                    chk64.IsChecked = true;
                }
                else
                {
                    chk64.IsChecked = true;
                    chk64.IsChecked = false;
                }
            }

            if (chk86 != null)
            {
                if (chk86.IsChecked == true)
                {
                    chk86.IsChecked = false;
                    chk86.IsChecked = true;
                }
                else
                {
                    chk86.IsChecked = true;
                    chk86.IsChecked = false;
                }
            }

            if (chkArm != null)
            {
                if (chkArm.IsChecked == true)
                {
                    chkArm.IsChecked = false;
                    chkArm.IsChecked = true;
                }
                else
                {
                    chkArm.IsChecked = true;
                    chkArm.IsChecked = false;
                }
            }
        }
        private string GetRuntimeIdentifiers(string platforms)
        {
            string rid = "win-x86;win-x64;win-arm64";
            if (!platforms.Contains("x86"))
            {
                rid = rid.Replace("win-x86;", "");
            }

            if (!platforms.Contains("x64"))
            {
                rid = rid.Replace("win-x64;", "");
            }

            if (!platforms.Contains("ARM64"))
            {
                rid = rid.Replace("win-arm64", "");
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

            if (chk.Tag == null)
            {
                return;
            }
            string tag = chk.Tag.ToString();

            if (chk.IsChecked == true && !platformList.Contains(tag))
            {
                platformList.Add(tag);
            }
            else if (chk.IsChecked == false && platformList.Contains(tag))
            {
                platformList.Remove(tag);
            }

            var platforms = GetPlatforms();

            WizardConfig.Platforms = platforms;
            WizardConfig.RuntimeIdentifiers = GetRuntimeIdentifiers(platforms);
        }

        private string GetPlatforms()
        {
            if (platformList.Contains("ARM64"))
            {
                platformList.Remove("ARM64");
                platformList.Add("ARM64");
            }

            string resultString = string.Join(";", platformList);

            if (string.IsNullOrEmpty(resultString))
            {
                resultString = "x86;x64;ARM64";
            }

            return resultString;
        }

        private void tgAccelerateBuilds_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseAccelerateBuilds = tgAccelerateBuilds.IsOn;
        }

        private void tgJsonSettings_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseJsonSettings = tgJsonSettings.IsOn;
        }

        private void tgDynamicLocalization_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseDynamicLocalization = tgDynamicLocalization.IsOn;
        }

        private void tgEditorConfig_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseEditorConfig = tgEditorConfig.IsOn;
        }

        private void tgSolutionFolder_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseSolutionFolder = tgSolutionFolder.IsOn;
        }

        private void tgGithubWorkflow_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardConfig.UseGithubWorkflow = tgGithubWorkflow.IsOn;
        }
    }
}
