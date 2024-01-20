using Microsoft.UI.Xaml.Media;using Windows.Storage;

namespace $safeprojectname$;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    public IThemeService ThemeService { get; set; }
    public IJsonNavigationViewService JsonNavigationViewService { get; set; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = AssemblyInfoHelper.GetAssemblyVersion();
    public string AppName { get; set; } = "$safeprojectname$";
    private static string StringsFolderPath { get; set; } = string.Empty;
    public App()
    {
        this.InitializeComponent();
        JsonNavigationViewService = new JsonNavigationViewService();$Configs$
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

        ThemeService = new ThemeService();
        ThemeService.Initialize(CurrentWindow);
        ThemeService.ConfigBackdrop();
        ThemeService.ConfigElementTheme();$BackdropTintColor$
        ThemeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);

        rootFrame.Navigate(typeof(MainPage));

        CurrentWindow.Title = CurrentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        CurrentWindow.AppWindow.SetIcon("Assets/icon.ico");$AppCenter$$ConfigLogger$

        CurrentWindow.Activate(); await InitializeLocalizer("en-US");$UnhandeledException$
    }private void InitializeLocalizer { };
}

