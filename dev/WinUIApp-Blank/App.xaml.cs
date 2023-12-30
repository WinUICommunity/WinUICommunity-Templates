namespace $safeprojectname$;

public partial class App : Application
{
    public static Window currentWindow = Window.Current;
    
    public App()
    {
        this.InitializeComponent();
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        currentWindow = new Window();

        if (currentWindow.Content is not Frame rootFrame)
        {
            currentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));

        currentWindow.Activate();
    }
}

