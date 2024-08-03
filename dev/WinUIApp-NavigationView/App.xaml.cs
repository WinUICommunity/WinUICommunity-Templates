namespace $safeprojectname$;

public partial class App : Application
{
    public static Window MainWindow = Window.Current;
    public IThemeService ThemeService { get; set; }
    public IJsonNavigationViewService JsonNavigationViewService { get; set; }
    public new static App Current => (App)Application.Current;

    public App()
    {
        this.InitializeComponent();
        JsonNavigationViewService = new JsonNavigationViewService();$Configs$
    }

    protected $OnLaunchedAsyncKeyword$override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new Window();
       
        if (MainWindow.Content is not Frame rootFrame)
        {
            MainWindow.Content = rootFrame = new Frame();
        }

        ThemeService = new ThemeService(MainWindow);

        rootFrame.Navigate(typeof(MainPage));

        MainWindow.Title = MainWindow.AppWindow.Title = ProcessInfoHelper.GetProductNameAndVersion();
        MainWindow.AppWindow.SetIcon("Assets/icon.ico");$ConfigLogger$

        MainWindow.Activate();$Windows11ContextMenuInitializer$$UnhandeledException$
    }
}

