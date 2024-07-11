using System;

namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        App.CurrentWindow.ExtendContentIntoTitleBar = true;
        App.CurrentWindow.SetTitleBar(AppTitleBar);
    }
}

