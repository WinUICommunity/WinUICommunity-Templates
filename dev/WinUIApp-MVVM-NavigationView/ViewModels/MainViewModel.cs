﻿namespace $safeprojectname$.ViewModels;
public partial class MainViewModel : ObservableObject, ITitleBarAutoSuggestBoxAware
{
    public IJsonNavigationViewService JsonNavigationViewService;
    public MainViewModel(IJsonNavigationViewService jsonNavigationViewService, IThemeService themeService)
    {
        JsonNavigationViewService = jsonNavigationViewService;
        themeService.Initialize(App.CurrentWindow);
        themeService.ConfigBackdrop();
        themeService.ConfigElementTheme();
    }

    public void OnAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {

    }

    public void OnAutoSuggestBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {

    }
}
