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
            catch (Exception)
            {

            }
        }

        private void AddOrUpdateElementWithOnOffContent(OptionUC optionUC)
        {
            try
            {
                var key = optionUC.Tag.ToString();
                var onValue = optionUC.OnContent;
                var offValue = optionUC.OffContent;

                WizardConfig.CSProjectElements.TryGetValue(key, out var valueExist);
                if (!string.IsNullOrEmpty(valueExist))
                {
                    if (optionUC.IsOn)
                    {
                        WizardConfig.CSProjectElements.Update(key, $"<{key}>{onValue}</{key}>");
                    }
                    else
                    {
                        WizardConfig.CSProjectElements.Update(key, $"<{key}>{offValue}</{key}>");
                    }
                }
                else
                {
                    if (optionUC.IsOn)
                    {
                        WizardConfig.CSProjectElements.AddIfNotExists(key, $"<{key}>{onValue}</{key}>");
                    }
                    else
                    {
                        WizardConfig.CSProjectElements.AddIfNotExists(key, $"<{key}>{offValue}</{key}>");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void TrimToggled(object sender, RoutedEventArgs e)
        {
            Toggled(sender, e);

            if (PublishTrimOption.IsOn)
            {
                AddOrUpdateElementWithOnOffContent(TrimModeOption);
            }
            else
            {
                TrimModeOption_Toggled(null, null);
            }
        }

        private void TrimModeOption_Toggled(object sender, RoutedEventArgs e)
        {
            var key = TrimModeOption.Tag.ToString();

            if (PublishTrimOption.IsOn)
            {
                AddOrUpdateElementWithOnOffContent(TrimModeOption);
            }
            else
            {
                WizardConfig.CSProjectElements.TryGetValue(key, out var valueExist);
                if (!string.IsNullOrEmpty(valueExist))
                {
                    WizardConfig.CSProjectElements.Remove(key);
                }
            }
        }
    }
}
