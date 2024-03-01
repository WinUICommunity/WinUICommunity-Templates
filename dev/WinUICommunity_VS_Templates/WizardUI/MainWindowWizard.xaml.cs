﻿using System.Windows.Controls;
using System.Windows;
using WinUICommunity_VS_Templates.WizardUI;
using iNKORE.UI.WPF.Modern.Controls;
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
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new System.Uri("/WinUICommunity_VS_Templates;component/WizardUI/ThemeResources.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new XamlControlsResources());

            InitializeComponent();
            Loaded += MainWindowWizard_Loaded;
        }

        private void MainWindowWizard_Loaded(object sender, RoutedEventArgs e)
        {
            nviPage.IsEnabled = WizardConfig.HasPages;
            if (WizardConfig.IsBlank)
            {
                nviPage.IsEnabled = false;
            }
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

            if (PlatformPage.Instance != null)
            {
                PlatformPage.Instance.UpdateCheckBoxs();
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

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
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

        private void cmbTargetFrameworkVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WizardConfig.TargetFrameworkVersion = (cmbTargetFrameworkVersion.SelectedItem as ComboBoxItem).Tag.ToString();
        }

        private void tgUnPackaged_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.IsUnPackagedMode = tgUnPackaged.IsOn;
        }
    }
}
