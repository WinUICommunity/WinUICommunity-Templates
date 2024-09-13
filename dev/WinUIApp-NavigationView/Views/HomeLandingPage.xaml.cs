namespace $safeprojectname$.Views;

public sealed partial class HomeLandingPage : Page
{
    public HomeLandingPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        allLandingPage.GetData(App.Current.JsonNavigationViewService.DataSource);
        allLandingPage.OrderBy(i => i.Title);
    }

    private void allLandingPage_OnItemClick(object sender, RoutedEventArgs e)
    {
        var args = (ItemClickEventArgs)e;
        var item = (DataItem)args.ClickedItem;

        App.Current.JsonNavigationViewService.NavigateTo(item.UniqueId + item.Parameter?.ToString(), item);
    }
}

