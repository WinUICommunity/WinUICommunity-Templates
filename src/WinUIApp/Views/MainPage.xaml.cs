namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        appTitleBar.Window = App.currentWindow;
    }
}

