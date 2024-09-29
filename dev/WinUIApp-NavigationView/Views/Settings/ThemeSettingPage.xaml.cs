namespace $safeprojectname$.Views;

public sealed partial class ThemeSettingPage : Page
{
    public ThemeSettingPage()
    {
        this.InitializeComponent();
        Loaded += ThemeSettingPage_Loaded;
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
}


