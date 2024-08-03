namespace $safeprojectname$;

public partial class App : Application
{
    public static Window MainWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;

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
        services.AddSingleton<IJsonNavigationViewService>(factory =>
        {
            var json = new JsonNavigationViewService();$Configs$
            return json;
        });

        services.AddTransient<MainViewModel>();$Services$

        return services.BuildServiceProvider();
    }

    protected $OnLaunchedAsyncKeyword$override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new Window();
        
        if (MainWindow.Content is not Frame rootFrame)
        {
            MainWindow.Content = rootFrame = new Frame();
        }

        var themeService = GetService<IThemeService>() as ThemeService;
        if (themeService != null)
        {
            themeService.AutoInitialize(MainWindow);
        }$Windows11ContextMenuMVVMInitializer$

        rootFrame.Navigate(typeof(MainPage));

        MainWindow.Title = MainWindow.AppWindow.Title = ProcessInfoHelper.ProductNameAndVersion;
        MainWindow.AppWindow.SetIcon("Assets/icon.ico");$ConfigLogger$

        MainWindow.Activate();$UnhandeledException$
    }
}

