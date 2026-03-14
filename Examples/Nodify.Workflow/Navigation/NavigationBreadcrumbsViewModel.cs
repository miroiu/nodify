using ObservableCollections;
using R3;

namespace Nodify.Workflow.Navigation;

public class NavigationBreadcrumbsViewModel
{
    public NotifyCollectionChangedSynchronizedViewList<NavigationBreadcrumbViewModel> Breadcrumbs { get; }

    public NavigationBreadcrumbsViewModel(NavigationService navigationService)
    {
        NavigationEntry? defaultEntry = navigationService.CurrentEntry.Value;
        Breadcrumbs = new ObservableList<NavigationBreadcrumbViewModel>(defaultEntry != null ? GetBreadcrumbsForEntry(defaultEntry, navigationService) : [])
            .ToWritableNotifyCollectionChanged();

        navigationService.CurrentEntry.Subscribe(entry => UpdateBreadcrumbsFromEntry(entry ?? new NavigationEntry(new object(), string.Empty, 0), navigationService));
    }

    private void UpdateBreadcrumbsFromEntry(NavigationEntry value, NavigationService navigationService)
    {
        Breadcrumbs.Clear();
        Breadcrumbs.AddRange(GetBreadcrumbsForEntry(value, navigationService));
    }

    private static NavigationBreadcrumbViewModel[] GetBreadcrumbsForEntry(NavigationEntry entry, NavigationService navigationService)
    {
        var splits = entry.RouteKey.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var routes = splits.Select((_, i) => string.Join("/", splits.Take(i + 1)));

        return routes.Select((route, index) => new NavigationBreadcrumbViewModel
        {
            RouteKey = GetLabel(route),
            Command = index == splits.Length - 1 ? null : new ReactiveCommand(_ => navigationService.Navigate(route, layer: entry.Layer)),
            IsRoot = index == 0
        }).ToArray();
    }

    private static string GetLabel(string route)
    {
        return route.Split('/').Last();
    }
}

public class NavigationBreadcrumbViewModel
{
    public bool IsRoot { get; init; }

    public required string RouteKey { get; init; }

    public ReactiveCommand? Command { get; init; }
}
