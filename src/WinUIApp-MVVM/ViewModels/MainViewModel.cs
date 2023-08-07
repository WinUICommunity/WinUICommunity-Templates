using Microsoft.UI.Xaml.Media;

namespace $safeprojectname$.ViewModels;
public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IThemeService themeService)
    {
        themeService.Initialize(App.currentWindow);
        themeService.ConfigBackdrop();
        themeService.ConfigElementTheme();
        themeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);
    }
}