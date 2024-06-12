namespace $safeprojectname$;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        CurrentWindow = new Window();

        if (CurrentWindow.Content is not Frame rootFrame)
        {
            CurrentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));
        CurrentWindow.Activate();
    }
}

