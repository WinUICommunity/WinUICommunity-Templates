using Microsoft.UI.Xaml.Media;

namespace $safeprojectname$.ViewModels;
public partial class MainViewModel : ObservableObject, ITitleBarAutoSuggestBoxAware
{
    public MainViewModel(IThemeService themeService)
    {
        themeService.Initialize(App.currentWindow);
        themeService.ConfigBackdrop();
        themeService.ConfigElementTheme();
        themeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);
    }

    public void OnAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        
    }

    public void OnAutoSuggestBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        
    }
}
