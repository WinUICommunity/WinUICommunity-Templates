namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        App.MainWindow.ExtendContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.Current.JsonNavigationViewService.Initialize(NavView, NavFrame);
        App.Current.JsonNavigationViewService.ConfigJson("Assets/NavViewMenu/AppData.json");
    }

    private void AppTitleBar_BackButtonClick(object sender, RoutedEventArgs e)
    {
        if (NavFrame.CanGoBack)
        {
            NavFrame.GoBack();
        }
    }

    private void AppTitleBar_PaneButtonClick(object sender, RoutedEventArgs e)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void NavFrame_Navigated(object sender, NavigationEventArgs e)
    {
        AppTitleBar.IsBackButtonVisible = NavFrame.CanGoBack;
    }

    private void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        var element = App.CurrentWindow.Content as FrameworkElement;

        if (element.ActualTheme == ElementTheme.Light)
        {
            element.RequestedTheme = ElementTheme.Dark;
        }
        else if (element.ActualTheme == ElementTheme.Dark)
        {
            element.RequestedTheme = ElementTheme.Light;
        }
    }
}

