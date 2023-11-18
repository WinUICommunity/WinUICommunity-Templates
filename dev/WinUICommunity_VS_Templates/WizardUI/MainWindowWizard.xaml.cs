using System.Windows.Controls;
using System.Windows;
using WinUICommunity_VS_Templates.WizardUI;
using System;
using iNKORE.UI.WPF.Modern;

namespace WinUICommunity_VS_Templates
{
    public partial class MainWindowWizard : Window
    {
        public MainWindowWizard()
        {
            InitializeComponent();
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

        private void cmbVersionMechanism_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WizardConfig.UseAlwaysLatestVersion = cmbVersionMechanism.SelectedIndex == 1;
            if (LibrariesPage.Instance != null)
            {
                LibrariesPage.Instance.CreateBoxes();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            DialogResult = false;
        }

        private void NavigationView_SelectionChanged(iNKORE.UI.WPF.Modern.NavigationView sender, iNKORE.UI.WPF.Modern.NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem;
            if (item != null && item is NavigationViewItem navigationViewItem && navigationViewItem.Tag != null)
            {
                switch (navigationViewItem.Tag.ToString())
                {
                    case "PlatformPage":
                        frame.Navigate(typeof(PlatformPage));
                        break;
                    case "ResourcePage":
                        frame.Navigate(typeof(ResourcePage));
                        break;
                    case "LibrariesPage":
                        frame.Navigate(typeof(LibrariesPage));
                        break;
                    case "PagesPages":
                        frame.Navigate(typeof(PagesPages));
                        break;
                }
            }
        }
    }
}
