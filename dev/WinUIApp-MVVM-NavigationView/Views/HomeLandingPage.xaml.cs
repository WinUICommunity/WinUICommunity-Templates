namespace $safeprojectname$.Views;

public sealed partial class HomeLandingPage : Page
{
    public HomeLandingViewModel ViewModel { get; }
    public HomeLandingPage()
    {
        ViewModel = App.GetService<HomeLandingViewModel>();
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        allLandingPage.GetData(ViewModel.JsonNavigationViewService.DataSource);
        allLandingPage.OrderBy(i => i.Title);
    }
}

