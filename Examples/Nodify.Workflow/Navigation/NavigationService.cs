using ObservableCollections;
using R3;

namespace Nodify.Workflow.Navigation;

public enum NavigationDirection
{
    Backward,
    Forward,
}

public class NavigationEntry(object viewModel, string routeKey, int layer)
{
    public object ViewModel { get; } = viewModel;

    public string RouteKey { get; } = routeKey;

    public int Layer { get; } = layer;
}

public class NavigationEventArgs(NavigationEntry? oldEntry, NavigationEntry newEntry, NavigationDirection direction) : EventArgs
{
    public NavigationEntry? OldEntry { get; } = oldEntry;

    public NavigationEntry NewEntry { get; } = newEntry;

    public NavigationDirection Direction { get; } = direction;

    public bool IsSameLayer => OldEntry?.Layer == NewEntry?.Layer;
}

public delegate object ViewModelFactory(string routeKey);

public class NavigationService
{
    private int _layer;
    private readonly ObservableList<NavigationEntry> _stack = [];
    private readonly ViewModelFactory _viewModelFactory;

    public IReadOnlyObservableList<NavigationEntry> Stack => _stack;

    public BindableReactiveProperty<object?> CurrentView { get; } = new();
    public BindableReactiveProperty<NavigationEntry?> CurrentEntry { get; } = new();

    public event EventHandler<NavigationEventArgs>? Navigating;
    public event EventHandler<NavigationEventArgs>? Navigated;

    public ReactiveCommand NavigateBackCommand { get; }

    public BindableReactiveProperty<bool> CanNavigateBack { get; } = new();

    public NavigationService(ViewModelFactory viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;

        NavigateBackCommand = CanNavigateBack.ToReactiveCommand(OnNavigateBack);
    }

    private void OnNavigateBack(Unit unit)
    {
        NavigateBack();
    }

    public void Navigate(string routeKey, int layer = 0, bool replace = false)
    {
        if (CanNavigateBack.Value && routeKey == _stack[^1].RouteKey)
        {
            return;
        }

        var oldEntry = GetCurrentEntry();
        var newEntry = _stack.FirstOrDefault(e => e.RouteKey == routeKey)
            ?? new NavigationEntry(_viewModelFactory(routeKey), routeKey, layer);

        OnNavigating(oldEntry, newEntry, NavigationDirection.Forward);

        if (replace)
        {
            _stack.RemoveAt(_stack.Count - 1);
        }

        _stack.Add(newEntry);

        UpdateCurrentView();

        OnNavigated(oldEntry, newEntry, NavigationDirection.Forward);
    }

    public void NavigateBack()
    {
        if (!CanNavigateBack.Value)
        {
            return;
        }

        var oldEntry = _stack[^1];
        var newEntry = _stack[^2];

        OnNavigating(oldEntry, newEntry, NavigationDirection.Backward);

        _stack.RemoveAt(_stack.Count - 1);

        UpdateCurrentView();

        OnNavigated(oldEntry, newEntry, NavigationDirection.Backward);
    }

    public void Clear()
    {
        _stack.Clear();

        UpdateCurrentView();
    }

    private NavigationEntry? GetCurrentEntry()
    {
        return _stack.Count > 0 ? _stack[^1] : null;
    }

    private void UpdateCurrentView()
    {
        var entry = GetCurrentEntry();

        CurrentEntry.Value = entry;
        CurrentView.Value = entry?.ViewModel;

        CanNavigateBack.Value = _stack.Count > 1;
    }

    private void OnNavigating(NavigationEntry? oldEntry, NavigationEntry newEntry, NavigationDirection direction)
    {
        Navigating?.Invoke(this, new NavigationEventArgs(oldEntry, newEntry, direction));
    }

    private void OnNavigated(NavigationEntry? oldEntry, NavigationEntry newEntry, NavigationDirection direction)
    {
        Navigated?.Invoke(this, new NavigationEventArgs(oldEntry, newEntry, direction));
    }
}
