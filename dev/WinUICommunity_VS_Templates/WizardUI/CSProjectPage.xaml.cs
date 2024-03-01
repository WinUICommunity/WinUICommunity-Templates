using System;
using System.Windows;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class CSProjectPage : Page
    {
        public CSProjectPage()
        {
            InitializeComponent();
        }

        private void tgIncludeNativeLibrariesForSelfExtract_Toggled(object sender, RoutedEventArgs e)
        {
            AddOrRemoveElement(tgIncludeNativeLibrariesForSelfExtract, "IncludeNativeLibrariesForSelfExtract");
        }

        private void tgIncludeAllContentForSelfExtract_Toggled(object sender, RoutedEventArgs e)
        {
            AddOrRemoveElement(tgIncludeAllContentForSelfExtract, "IncludeAllContentForSelfExtract");
        }

        private void AddOrRemoveElement(OptionUC optionUC, string keyValue)
        {
            try
            {
                if (optionUC.IsOn)
                {
                    WizardConfig.CSProjectElements.AddIfNotExists(keyValue, $"<{keyValue}>true</{keyValue}>");
                }
                else
                {
                    WizardConfig.CSProjectElements.TryGetValue(keyValue, out var valueExist);
                    if (!string.IsNullOrEmpty(valueExist))
                    {
                        WizardConfig.CSProjectElements.Remove(keyValue);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
