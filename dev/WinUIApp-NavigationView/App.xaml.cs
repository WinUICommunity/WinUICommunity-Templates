using Microsoft.UI.Xaml.Media;

namespace $safeprojectname$;

public partial class App : Application
{
    public static Window currentWindow = Window.Current;
    public IThemeService ThemeService { get; set; }
    public IJsonNavigationViewService JsonNavigationViewService { get; set; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = VersionHelper.GetVersion();
    public string AppName { get; set; } = "$safeprojectname$";

    public App()
    {
        this.InitializeComponent();
        JsonNavigationViewService = new JsonNavigationViewService();
        JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));
        JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        currentWindow = new Window();

        ThemeService = new ThemeService();
        ThemeService.Initialize(currentWindow);
        ThemeService.ConfigBackdrop();
        ThemeService.ConfigElementTheme();
        ThemeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);

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
}

