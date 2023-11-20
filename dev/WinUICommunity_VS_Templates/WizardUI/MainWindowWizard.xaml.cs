using System.Windows.Controls;
using System.Windows;
using WinUICommunity_VS_Templates.WizardUI;
using iNKORE.UI.WPF.Modern;
using System;

namespace WinUICommunity_VS_Templates
{
    public partial class MainWindowWizard : Window
    {
        PlatformPage platformType;
        LibrariesPage librariesType;
        PagesPages pagesType;
        ResourcePage resourceType;
        public MainWindowWizard()
        {
            var lightResource = new ResourceDictionary { Source = new Uri("pack://application:,,,/WinUICommunity_VS_Templates;component/WizardShell/ThemeResources/Light.xaml") };
            var darkResource = new ResourceDictionary { Source = new Uri("pack://application:,,,/WinUICommunity_VS_Templates;component/WizardShell/ThemeResources/Dark.xaml") };

            InitializeComponent();
            Loaded += MainWindowWizard_Loaded;
            var actualTheme = ThemeManager.GetActualTheme(this);
            if (actualTheme == ElementTheme.Dark)
            {
                Application.Current.Resources.MergedDictionaries.Add(darkResource);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(lightResource);
            }
        }

        private void MainWindowWizard_Loaded(object sender, RoutedEventArgs e)
        {
            nviPage.IsEnabled = WizardConfig.HasPages;
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

        private void cmbNetVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WizardConfig.DotNetVersion = (cmbNetVersion.SelectedItem as ComboBoxItem).Tag.ToString();
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
            Resources?.Clear();
            Application.Current?.Resources?.Clear();
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
                        if (platformType == null)
                        {
                            platformType = new PlatformPage();
                        }
                        frame.Navigate(platformType);
                        break;
                    case "ResourcePage":
                        if (resourceType == null)
                        {
                            resourceType = new ResourcePage();
                        }
                        frame.Navigate(resourceType);
                        break;
                    case "LibrariesPage":
                        if (librariesType == null)
                        {
                            librariesType = new LibrariesPage();
                        }
                        frame.Navigate(librariesType);
                        break;
                    case "PagesPages":
                        if (pagesType == null)
                        {
                            pagesType = new PagesPages();
                        }
                        frame.Navigate(pagesType);
                        break;
                }
            }
        }
    }
}
