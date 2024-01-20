using Microsoft.UI.Xaml.Media;
using Windows.System;

namespace $safeprojectname$.Views;

public sealed partial class ThemeSettingPage : Page
{
    public string BreadCrumbBarItemText { get; set; }

    public ThemeSettingPage()
    {
        this.InitializeComponent();
        Loaded += ThemeSettingPage_Loaded;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        BreadCrumbBarItemText = e.Parameter as string;
    }

    private void ThemeSettingPage_Loaded(object sender, RoutedEventArgs e)
    {
        App.Current.ThemeService.SetThemeComboBoxDefaultItem(CmbTheme);
        App.Current.ThemeService.SetBackdropComboBoxDefaultItem(CmbBackdrop);
    }

    private void CmbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App.Current.ThemeService.OnThemeComboBoxSelectionChanged(sender);
    }

    private void CmbBackdrop_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App.Current.ThemeService.OnBackdropComboBoxSelectionChanged(sender);
    }

    private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        TintBox.Fill = new SolidColorBrush(args.NewColor);
        App.Current.ThemeService.SetBackdropTintColor(args.NewColor);
    }

    private void ColorPalette_ItemClick(object sender, ItemClickEventArgs e)
    {
        var color = e.ClickedItem as ColorPaletteItem;
        if (color != null)
        {
            if (color.Hex.Contains("#000000"))
            {
                App.Current.ThemeService.ResetBackdropProperties();
            }
            else
            {
                App.Current.ThemeService.SetBackdropTintColor(color.Color);
            }

            TintBox.Fill = new SolidColorBrush(color.Color);
        }
    }

    private async void OpenWindowsColorSettings(object sender, RoutedEventArgs e)
    {
        _ = await Launcher.LaunchUriAsync(new Uri("ms-settings:colors"));
    }
}


