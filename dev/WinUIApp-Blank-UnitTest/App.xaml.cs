namespace $safeprojectname$;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    
    public App()
    {
        this.InitializeComponent();
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.CreateDefaultUI();

        CurrentWindow = new Window();

        if (CurrentWindow.Content is not Frame rootFrame)
        {
            CurrentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));
        CurrentWindow.ExtendsContentIntoTitleBar = true;
        CurrentWindow.Activate();
        
        UITestMethodAttribute.DispatcherQueue = CurrentWindow.DispatcherQueue;
        Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.Run(Environment.CommandLine);
    }
}

