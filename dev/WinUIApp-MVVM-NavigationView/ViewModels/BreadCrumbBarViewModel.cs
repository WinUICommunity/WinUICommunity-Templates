using System.Collections.ObjectModel;

namespace $safeprojectname$.ViewModels;
public partial class BreadCrumbBarViewModel : ObservableRecipient
{
    [ObservableProperty]
    public ObservableCollection<string> breadcrumbBarCollection;

    public IJsonNavigationViewService JsonNavigationViewService;

    public BreadCrumbBarViewModel(IJsonNavigationViewService jsonNavigationViewService)
    {
        JsonNavigationViewService = jsonNavigationViewService;
        breadcrumbBarCollection = new ObservableCollection<string>();
    }

    [RelayCommand]
    private void OnItemClick(BreadcrumbBarItemClickedEventArgs args)
    {
        int numItemsToGoBack = BreadcrumbBarCollection.Count - args.Index - 1;
        for (int i = 0; i < numItemsToGoBack; i++)
        {
            JsonNavigationViewService.GoBack();
        }
    }
}
