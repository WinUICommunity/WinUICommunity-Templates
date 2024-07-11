namespace $safeprojectname$;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    public IThemeService ThemeService { get; set; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = AssemblyInfoHelper.GetAssemblyVersion();
    public string AppName { get; set; } = "$safeprojectname$";
    public App()
    {
        this.InitializeComponent();
    }

    protected $OnLaunchedAsyncKeyword$override void OnLaunched(LaunchActivatedEventArgs args)
    {
        CurrentWindow = new Window();

        if (CurrentWindow.Content is not Frame rootFrame)
        {
            CurrentWindow.Content = rootFrame = new Frame();
        }

        ThemeService = new ThemeService();
        ThemeService.Initialize(CurrentWindow);
        ThemeService.ConfigBackdrop();
        ThemeService.ConfigElementTheme();

        rootFrame.Navigate(typeof(MainPage));

        CurrentWindow.Title = CurrentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        CurrentWindow.AppWindow.SetIcon("Assets/icon.ico");$ConfigLogger$

        CurrentWindow.Activate();$Windows11ContextMenuInitializer$$UnhandeledException$
    }
}

