using FluentIcons.Common;
using Nodify.Interactivity;
using ObservableCollections;
using R3;

namespace Nodify.Workflow.Settings
{
    internal class KeybindingViewModel(string label, string description, Icon icon, InputGestureRef gestureRef)
    {
        public BindableReactiveProperty<string> Label { get; } = new(label);
        public BindableReactiveProperty<string> Description { get; } = new(description);
        public BindableReactiveProperty<Icon> Icon { get; } = new(icon);

        public GestureSelectorViewModel GestureEditor { get; set; } = new KeyGestureSelectorViewModel(gestureRef);
    }

    internal class KeybindingsListViewModel
    {
        public BindableReactiveProperty<string> Title { get; } = new(string.Empty);

        public ObservableList<KeybindingViewModel> Keybindings { get; } = [];
    }

    internal class KeybindingCategoryViewModel(string name, string description, Icon icon, ReactiveCommand selectCommand)
    {
        public BindableReactiveProperty<string> Name { get; } = new(name);
        public BindableReactiveProperty<string> Description { get; } = new(description);
        public BindableReactiveProperty<Icon> Icon { get; } = new(icon);
        public ReactiveCommand SelectCommand { get; } = selectCommand;
    }
}
