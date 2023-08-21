using Windows.Storage;namespace $safeprojectname$;

public partial class App : Application
{
    public static Window currentWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = VersionHelper.GetVersion();
    public string AppName { get; set; } = "$safeprojectname$";
    private static string StringsFolderPath { get; set; } = string.Empty;
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

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IThemeService, ThemeService>();

        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
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

        currentWindow.Activate(); await InitializeLocalizer("en-US");
    }private void InitializeLocalizer { };
}

