using Windows.System;

namespace $safeprojectname$.ViewModels;
public partial class AppUpdateSettingViewModel : ObservableObject
{
    [ObservableProperty]
    public string currentVersion;

    [ObservableProperty]
    public string lastUpdateCheck;

    [ObservableProperty]
    public bool isUpdateAvailable;

    [ObservableProperty]
    public bool isLoading;

    [ObservableProperty]
    public bool isCheckButtonEnabled = true;

    [ObservableProperty]
    public string loadingStatus = "Status";

    private string ChangeLog = string.Empty;

    public AppUpdateSettingViewModel()
    {
        CurrentVersion = $"Current Version v{App.Current.AppVersion}";
        LastUpdateCheck = Settings.LastUpdateCheck;
    }

    [RelayCommand]
    private async void CheckForUpdateAsync()
    {
        IsLoading = true;
        IsUpdateAvailable = false;
        IsCheckButtonEnabled = false;
        LoadingStatus = "Checking for new version";
        if (ApplicationHelper.IsNetworkAvailable())
        {
            LastUpdateCheck = DateTime.Now.ToShortDateString();
            Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();

            try
            {
                //Todo: Fix UserName and Repo
                string username = "";
                string repo = "";
                var update = await UpdateHelper.CheckUpdateAsync(username, repo, new Version(App.Current.AppVersion));
                if (update.IsExistNewVersion)
                {
                    IsUpdateAvailable = true;
                    ChangeLog = update.Changelog;
                    LoadingStatus = $"We found a new version {update.TagName} Created at {update.CreatedAt} and Published at {update.PublishedAt}";
                }
                else
                {
                    LoadingStatus = "You are using latest version";
                }
            }
            catch (Exception ex)
            {
                LoadingStatus = ex.Message;
                IsLoading = false;
                IsCheckButtonEnabled = true;
            }
        }
        else
        {
            LoadingStatus = "Error Connection";
        }
        IsLoading = false;
        IsCheckButtonEnabled = true;
    }

    [RelayCommand]
    private async void GoToUpdateAsync()
    {
        //Todo: Change Uri
        await Launcher.LaunchUriAsync(new Uri("https://github.com/WinUICommunity/WinUICommunity/releases"));
    }

    [RelayCommand]
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
