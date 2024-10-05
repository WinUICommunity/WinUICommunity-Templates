namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel { get; }
    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        this.InitializeComponent();
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);

        var jsonNavigationViewService = App.GetService<IJsonNavigationViewService>() as JsonNavigationViewService;
        if (jsonNavigationViewService != null)
        {
            jsonNavigationViewService.Initialize(NavView, NavFrame);
            jsonNavigationViewService.ConfigJson("Assets/NavViewMenu/AppData.json");
            jsonNavigationViewService.ConfigBreadcrumbBar(JsonBreadCrumbNavigator);
        }
    }

    private void AppTitleBar_BackRequested(Microsoft.UI.Xaml.Controls.TitleBar sender, object args)
    {
        if (NavFrame.CanGoBack)
        {
            NavFrame.GoBack();
        }
    }

    private void AppTitleBar_PaneToggleRequested(Microsoft.UI.Xaml.Controls.TitleBar sender, object args)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void NavFrame_Navigated(object sender, NavigationEventArgs e)
    {
        AppTitleBar.IsBackButtonVisible = NavFrame.CanGoBack;
    }

    private void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        var element = App.MainWindow.Content as FrameworkElement;

        if (element.ActualTheme == ElementTheme.Light)
        {
            element.RequestedTheme = ElementTheme.Dark;
        }
        else if (element.ActualTheme == ElementTheme.Dark)
        {
            element.RequestedTheme = ElementTheme.Light;
        }
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        var viewModel = NavFrame.GetPageViewModel();
        if (viewModel != null && viewModel is ITitleBarAutoSuggestBoxAware titleBarAutoSuggestBoxAware)
        {
            titleBarAutoSuggestBoxAware.OnAutoSuggestBoxTextChanged(sender, args);
        }
    }

    private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var viewModel = NavFrame.GetPageViewModel();
        if (viewModel != null && viewModel is ITitleBarAutoSuggestBoxAware titleBarAutoSuggestBoxAware)
        {
            titleBarAutoSuggestBoxAware.OnAutoSuggestBoxQuerySubmitted(sender, args);
        }
    }
}

