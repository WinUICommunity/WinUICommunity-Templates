using Microsoft.UI.Xaml.Media;using Windows.Storage;

namespace $safeprojectname$;

public partial class App : Application
{
    public static Window currentWindow = Window.Current;
    public IThemeService ThemeService { get; set; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = AssemblyInfoHelper.GetAssemblyVersion();
    public string AppName { get; set; } = "$safeprojectname$";
    private static string StringsFolderPath { get; set; } = string.Empty;
    public App()
    {
        this.InitializeComponent();
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

        ThemeService = new ThemeService();
        ThemeService.Initialize(currentWindow);
        ThemeService.ConfigBackdrop();
        ThemeService.ConfigElementTheme();
        ThemeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);

        rootFrame.Navigate(typeof(MainPage));

        currentWindow.Title = currentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        currentWindow.AppWindow.SetIcon("Assets/icon.ico");$AppCenter$$ConfigLogger$

        currentWindow.Activate();await InitializeLocalizer("en-US");$UnhandeledException$
    }private void InitializeLocalizer { };
}

