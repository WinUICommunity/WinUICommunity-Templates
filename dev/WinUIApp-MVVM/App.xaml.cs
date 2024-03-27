using Windows.Storage;namespace $safeprojectname$;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = AssemblyInfoHelper.GetAssemblyVersion();
    public string AppName { get; set; } = "$safeprojectname$";
    private static string StringsFolderPath { get; set; } = string.Empty;
    public static T GetService<T>() where T : class
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
        CurrentWindow = new Window();
        
        CurrentWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        CurrentWindow.AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;

        if (CurrentWindow.Content is not Frame rootFrame)
        {
            CurrentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));

        CurrentWindow.Title = CurrentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        CurrentWindow.AppWindow.SetIcon("Assets/icon.ico");$ConfigLogger$

        CurrentWindow.Activate(); await InitializeLocalizer("en-US");$UnhandeledException$
    }private void InitializeLocalizer { };
}

