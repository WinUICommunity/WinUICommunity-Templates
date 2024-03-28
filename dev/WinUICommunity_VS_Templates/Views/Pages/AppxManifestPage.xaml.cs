﻿using System;
using System.Windows;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class AppxManifestPage : Page
    {
        public AppxManifestPage()
        {
            InitializeComponent();
        }

        private void Toggled(object sender, RoutedEventArgs e)
        {
            var optionUC = sender as OptionUC;
            AddOrRemoveElement(optionUC);
        }

        private void AddOrRemoveElement(OptionUC optionUC)
        {
            try
            {
                string keyValue = optionUC.Tag.ToString();
                if (optionUC.IsOn)
                {
                    WizardConfig.UnvirtualizedResources.TryGetValue(keyValue, out var valueExist);
                    if (!string.IsNullOrEmpty(valueExist))
                    {
                        WizardConfig.UnvirtualizedResources.Remove(keyValue);
                    }
                    WizardConfig.MinimumTargetPlatform = WizardConfig.MinimumTargetPlatformDefault;
                }
                else
                {
                    WizardConfig.UnvirtualizedResources.AddIfNotExists(keyValue, $"    <{keyValue}>disabled</{keyValue}>");
                    WizardConfig.MinimumTargetPlatform = "18362";
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
