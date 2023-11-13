using Windows.System;

namespace $safeprojectname$.Views;

public sealed partial class AppUpdateSettingPage : Page
{
    public string CurrentVersion { get; set; }
    public string ChangeLog { get; set; }
    public string BreadCrumbBarItemText { get; set; }

    public AppUpdateSettingPage()
    {
        this.InitializeComponent();
        CurrentVersion = $"Current Version v{App.Current.AppVersion}";

        TxtLastUpdateCheck.Text = Settings.LastUpdateCheck;

        BtnReleaseNote.Visibility = Visibility.Collapsed;
        BtnDownloadUpdate.Visibility = Visibility.Collapsed;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        BreadCrumbBarItemText = e.Parameter as string;
    }

    private async void CheckForUpdateAsync()
    {
        PrgLoading.IsActive = true;
        BtnCheckUpdate.IsEnabled = false;
        BtnReleaseNote.Visibility = Visibility.Collapsed;
        BtnDownloadUpdate.Visibility = Visibility.Collapsed;
        StatusCard.Header = "Checking for new version";
        if (NetworkHelper.IsNetworkAvailable())
        {
            TxtLastUpdateCheck.Text = DateTime.Now.ToShortDateString();
            Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();

            try
            {
                //Todo: Fix UserName and Repo
                string username = "";
                string repo = "";
                var update = await UpdateHelper.CheckUpdateAsync(username, repo, new Version(App.Current.AppVersion));
                if (update.IsExistNewVersion)
                {
                    BtnReleaseNote.Visibility = Visibility.Visible;
                    BtnDownloadUpdate.Visibility = Visibility.Visible;
                    ChangeLog = update.Changelog;
                    StatusCard.Header = $"We found a new version {update.TagName} Created at {update.CreatedAt} and Published at {update.PublishedAt}";
                }
                else
                {
                    StatusCard.Header = "You are using latest version";
                }
            }
            catch (Exception ex)
            {
                StatusCard.Header = ex.Message;
                PrgLoading.IsActive = false;
                BtnCheckUpdate.IsEnabled = true;
            }
        }
        else
        {
            StatusCard.Header = "Error Connection";
        }
        PrgLoading.IsActive = false;
        BtnCheckUpdate.IsEnabled = true;
    }

    private async void GoToUpdateAsync()
    {
        //Todo: Change Uri
        await Launcher.LaunchUriAsync(new Uri("https://github.com/WinUICommunity/WinUICommunity/releases"));
    }

    private async void GetReleaseNotesAsync()
    {
        ContentDialog dialog = new ContentDialog()
        {
            Title = "Release Note",
            CloseButtonText = "Close",
            Content = new ScrollViewer
            {
                Content = new TextBlock
                {
                    Text = ChangeLog,
                    Margin = new Thickness(10)
                },
                Margin = new Thickness(10)
            },
            Margin = new Thickness(10),
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = App.currentWindow.Content.XamlRoot
        };

        await dialog.ShowAsyncQueue();
    }
}


