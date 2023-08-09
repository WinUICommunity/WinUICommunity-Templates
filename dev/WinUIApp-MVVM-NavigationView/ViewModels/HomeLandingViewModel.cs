namespace $safeprojectname$.ViewModels;
public partial class HomeLandingViewModel : ObservableObject
{
    public IJsonNavigationViewService JsonNavigationViewService;
    public HomeLandingViewModel(IJsonNavigationViewService jsonNavigationViewService)
    {
        JsonNavigationViewService = jsonNavigationViewService;
    }

    [RelayCommand]
    private void OnItemClick(RoutedEventArgs e)
    {
        var args = (ItemClickEventArgs)e;
        var item = (DataItem)args.ClickedItem;

        JsonNavigationViewService.NavigateTo(item.UniqueId + item.Parameter?.ToString(), item);
    }
}