using FluentIcons.Common;
using Nodify.Workflow.Navigation;
using ObservableCollections;

namespace Nodify.Workflow.Settings;

internal class KeybindingsSettingsViewModel : INavigatable
{
    public static string RouteKey => "Keybindings";

    private readonly NavigationService _navigationService;

    public ObservableList<KeybindingCategoryViewModel> Categories { get; } = [];

    public KeybindingsSettingsViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;

        Categories.Add(new KeybindingCategoryViewModel("Editor", "Configure keybindings for editor actions", Icon.FlowDot, new(_ => NavigateTo(EditorKeybindingsListViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Item container", "Configure keybindings for item container actions", Icon.RectangleLandscapeSparkle, new(_ => NavigateTo(ItemContainerKeybindingsViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Connector", "Configure keybindings for connector actions", Icon.PlugDisconnected, new(_ => NavigateTo(ConnectorKeybindingsViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Connection", "Configure keybindings for connection actions", Icon.ArrowTurnBidirectionalDownRight, new(_ => NavigateTo(ConnectionKeybindingsViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Grouping Node", "Configure keybindings for grouping node actions", Icon.SquaresNested, new(_ => NavigateTo(GroupingNodeKeybindingsViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Minimap", "Configure keybindings for minimap actions", Icon.Map, new(_ => NavigateTo(MinimapKeybindingsViewModel.RouteKey))));
    }

    private void NavigateTo(string routeKey)
    {
        _navigationService.Navigate(routeKey, layer: 1);
    }
}