namespace $safeprojectname$;

public partial class App : Application
{
    public static Window currentWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = VersionHelper.GetVersion();
    public string AppName { get; set; } = "$safeprojectname$";

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        currentWindow = new Window();
        
        currentWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        currentWindow.AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;

        if (currentWindow.Content is not Frame rootFrame)
        {
            currentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));

        currentWindow.Title = currentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        currentWindow.AppWindow.SetIcon("Assets/icon.ico");

        currentWindow.Activate();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IJsonNavigationViewService>(factory =>
        {
            var json = new JsonNavigationViewService();
            json.ConfigDefaultPage(typeof(HomeLandingPage));
            json.ConfigSettingsPage(typeof(SettingsPage));
            return json;
        });
        services.AddTransient<HomeLandingViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<BreadCrumbBarViewModel>();

        //Settings
        services.AddTransient<AboutUsSettingViewModel>();
        services.AddTransient<ThemeSettingViewModel>();

        return services.BuildServiceProvider();
    }
}

