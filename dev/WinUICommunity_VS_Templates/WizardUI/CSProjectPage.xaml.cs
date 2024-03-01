using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinUICommunity_VS_Templates.WizardUI
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
