using FluentIcons.Common;
using Nodify.Interactivity;
using ObservableCollections;
using R3;

namespace Nodify.Workflow.Settings;

internal sealed class ApplicationSettingsViewModel
{
    public EditorGestures EditorGestures { get; } = new EditorGestures();

    public ObservableList<SettingsCategoryViewModel> Categories { get; } = [];

    public BindableReactiveProperty<SettingsCategoryViewModel> SelectedCategory { get; set; }

    public ApplicationSettingsViewModel()
    {
        Categories.Add(new SettingsCategoryViewModel("General", Icon.Settings));
        Categories.Add(new KeybindingsSettingsCategoryViewModel());
        Categories.Add(new SettingsCategoryViewModel("Appearance", Icon.Color));
        Categories.Add(new SettingsCategoryViewModel("About", Icon.Info));

        SelectedCategory = new(Categories[1]);
    }
}

internal class SettingsCategoryViewModel(string name, Icon icon)
{
    public BindableReactiveProperty<string> Name { get; } = new(name);
    public BindableReactiveProperty<Icon> Icon { get; } = new(icon);
}

internal class KeybindingCategoryViewModel(string name, string description, Icon icon)
{
    public BindableReactiveProperty<string> Name { get; } = new(name);
    public BindableReactiveProperty<string> Description { get; } = new(description);
    public BindableReactiveProperty<Icon> Icon { get; } = new(icon);
}

internal class KeybindingsSettingsCategoryViewModel : SettingsCategoryViewModel
{
    public ObservableList<KeybindingCategoryViewModel> Categories { get; } = [];

    public KeybindingsSettingsCategoryViewModel() : base("Keybindings", FluentIcons.Common.Icon.Keyboard)
    {
        Categories.Add(new KeybindingCategoryViewModel("Editor", "Configure keybindings for editor actions", FluentIcons.Common.Icon.FlowDot));
        Categories.Add(new KeybindingCategoryViewModel("Item Container", "Configure keybindings for item container actions", FluentIcons.Common.Icon.RectangleLandscapeSparkle));
        Categories.Add(new KeybindingCategoryViewModel("Connector", "Configure keybindings for connector actions", FluentIcons.Common.Icon.PlugDisconnected));
        Categories.Add(new KeybindingCategoryViewModel("Connection", "Configure keybindings for connection actions", FluentIcons.Common.Icon.ArrowTurnBidirectionalDownRight));
        Categories.Add(new KeybindingCategoryViewModel("Grouping Node", "Configure keybindings for grouping node actions", FluentIcons.Common.Icon.SquaresNested));
        Categories.Add(new KeybindingCategoryViewModel("Minimap", "Configure keybindings for minimap actions", FluentIcons.Common.Icon.Map));
    }
}