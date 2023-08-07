namespace $safeprojectname$.Views;

public sealed partial class HomeLandingPage : Page
{
    public string AppInfo { get; set; }
    public HomeLandingViewModel ViewModel { get; }
    public HomeLandingPage()
    {
        ViewModel = App.GetService<HomeLandingViewModel>();
        this.InitializeComponent();
        AppInfo = $"{App.Current.AppName} v{App.Current.AppVersion}";
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        allLandingPage.GetData(ViewModel.JsonNavigationViewService.DataSource);
        allLandingPage.OrderBy(i => i.Title);
    }
}

