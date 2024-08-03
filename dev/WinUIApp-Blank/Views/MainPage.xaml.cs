namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        App.MainWindow.ExtendContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
    }
}

