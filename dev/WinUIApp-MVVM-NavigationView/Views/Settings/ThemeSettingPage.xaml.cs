namespace $safeprojectname$.Views;

public sealed partial class ThemeSettingPage : Page
{
    public ThemeSettingViewModel ViewModel { get; }

    public ThemeSettingPage()
    {
        ViewModel = App.GetService<ThemeSettingViewModel>();
        this.InitializeComponent();
        Loaded += ThemeSettingPage_Loaded;
    }

    private void ThemeSettingPage_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.ThemeService.SetThemeComboBoxDefaultItem(CmbTheme);
        ViewModel.ThemeService.SetBackdropComboBoxDefaultItem(CmbBackdrop);
    }
}


