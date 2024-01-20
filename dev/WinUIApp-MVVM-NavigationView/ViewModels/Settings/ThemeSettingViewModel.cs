using Microsoft.UI.Xaml.Media;
using Windows.System;

namespace $safeprojectname$.ViewModels;
public partial class ThemeSettingViewModel : ObservableObject
{
    [ObservableProperty]
    public Brush previewTintColor;

    public IThemeService ThemeService;
    public ThemeSettingViewModel(IThemeService themeService)
    {
        ThemeService = themeService;
        PreviewTintColor = ThemeService.GetBackdropTintBrush();
    }

    [RelayCommand]
    private void OnBackdropChanged(object sender)
    {
        ThemeService.OnBackdropComboBoxSelectionChanged(sender);
    }

    [RelayCommand]
    private void OnThemeChanged(object sender)
    {
        ThemeService.OnThemeComboBoxSelectionChanged(sender);
    }

    [RelayCommand]
    private void OnColorPickerColorChanged(ColorChangedEventArgs args)
    {
        PreviewTintColor = new SolidColorBrush(args.NewColor);
        ThemeService.SetBackdropTintColor(args.NewColor);
    }

    [RelayCommand]
    private void OnColorPaletteItemClick(ItemClickEventArgs e)
    {
        var color = e.ClickedItem as ColorPaletteItem;
        if (color != null)
        {
            if (color.Hex.Contains("#000000"))
            {
                ThemeService.ResetBackdropProperties();
            }
            else
            {
                ThemeService.SetBackdropTintColor(color.Color);
            }

            PreviewTintColor = new SolidColorBrush(color.Color);
        }
    }

    [RelayCommand]
    private async Task OpenWindowsColorSettings()
    {
        _ = await Launcher.LaunchUriAsync(new Uri("ms-settings:colors"));
    }
}
