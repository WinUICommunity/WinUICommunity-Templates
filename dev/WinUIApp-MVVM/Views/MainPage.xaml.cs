namespace $safeprojectname$.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel { get; }
    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        this.InitializeComponent();
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
    }

    private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        var viewModel = Frame.GetPageViewModel();
        var frameContentAOTSafe = Frame?.Content;
        if (frameContentAOTSafe is Page page && page?.DataContext is ITitleBarAutoSuggestBoxAware viewModelAOTSafe)
        {
            viewModelAOTSafe.OnAutoSuggestBoxTextChanged(sender, args);
        }
        else if (viewModel != null && viewModel is ITitleBarAutoSuggestBoxAware titleBarAutoSuggestBoxAware)
        {
            titleBarAutoSuggestBoxAware.OnAutoSuggestBoxTextChanged(sender, args);
        }
    }

    private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var viewModel = Frame.GetPageViewModel();
        var frameContentAOTSafe = Frame?.Content;
        if (frameContentAOTSafe is Page page && page?.DataContext is ITitleBarAutoSuggestBoxAware viewModelAOTSafe)
        {
            viewModelAOTSafe.OnAutoSuggestBoxQuerySubmitted(sender, args);
        }
        else if (viewModel != null && viewModel is ITitleBarAutoSuggestBoxAware titleBarAutoSuggestBoxAware)
        {
            titleBarAutoSuggestBoxAware.OnAutoSuggestBoxQuerySubmitted(sender, args);
        }
    }
}

