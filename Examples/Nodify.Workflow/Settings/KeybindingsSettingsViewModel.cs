using FluentIcons.Common;
using Nodify.Interactivity;
using Nodify.Workflow.Navigation;
using ObservableCollections;

namespace Nodify.Workflow.Settings;

internal class KeybindingsSettingsViewModel : INavigatable
{
    public static string RouteKey => "Keybindings";

    private readonly NavigationService _navigationService;

    public ObservableList<KeybindingCategoryViewModel> Categories { get; } = [];

    public KeybindingsSettingsViewModel(EditorGestures gestures, NavigationService navigationService)
    {
        _navigationService = navigationService;

        Categories.Add(new KeybindingCategoryViewModel("Editor", "Configure keybindings for editor actions", Icon.FlowDot, new(_ => NavigateTo(EditorKeybindingsListViewModel.RouteKey))));
        Categories.Add(new KeybindingCategoryViewModel("Item Container", "Configure keybindings for item container actions", Icon.RectangleLandscapeSparkle, new(_ => NavigateTo("Item Container"))));
        Categories.Add(new KeybindingCategoryViewModel("Connector", "Configure keybindings for connector actions", Icon.PlugDisconnected, new(_ => NavigateTo("Connector"))));
        Categories.Add(new KeybindingCategoryViewModel("Connection", "Configure keybindings for connection actions", Icon.ArrowTurnBidirectionalDownRight, new(_ => NavigateTo("Connection"))));
        Categories.Add(new KeybindingCategoryViewModel("Grouping Node", "Configure keybindings for grouping node actions", Icon.SquaresNested, new(_ => NavigateTo("Grouping Node"))));
        Categories.Add(new KeybindingCategoryViewModel("Minimap", "Configure keybindings for minimap actions", Icon.Map, new(_ => NavigateTo("Minimap"))));
    }

    private void NavigateTo(string routeKey)
    {
        _navigationService.Navigate(routeKey, layer: 1);
    }
}