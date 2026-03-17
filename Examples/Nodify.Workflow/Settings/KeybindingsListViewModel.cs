using MouseGesture = Nodify.Interactivity.MouseGesture;
using FluentIcons.Common;
using Nodify.Interactivity;
using ObservableCollections;
using R3;

namespace Nodify.Workflow.Settings
{
    internal class KeybindingViewModel(string label, string description, InputGestureRef gestureRef, Icon? icon = null)
    {
        public BindableReactiveProperty<string> Label { get; } = new(label);
        public BindableReactiveProperty<string> Description { get; } = new(description);
        public BindableReactiveProperty<Icon?> Icon { get; } = new(icon);

        public GestureSelectorViewModel GestureEditor { get; set; } = gestureRef.Value switch
        {
            MouseGesture => new MouseGestureSelectorViewModel(gestureRef),
            System.Windows.Input.MouseGesture => new MouseGestureSelectorViewModel(gestureRef),
            MultiGesture multiGesture => multiGesture.Gestures.OfType<MouseGesture>().Any() ? new MouseGestureSelectorViewModel(gestureRef) : new KeyGestureSelectorViewModel(gestureRef),
            _ => new KeyGestureSelectorViewModel(gestureRef)
        };
    }

    internal class KeybindingsGroupViewModel(string label) : ObservableList<KeybindingViewModel>
    {
        public BindableReactiveProperty<string> Label { get; } = new(label);
    }

    internal class KeybindingsListViewModel
    {
        public BindableReactiveProperty<string> Title { get; } = new(string.Empty);

        public ObservableList<KeybindingsGroupViewModel> Keybindings { get; } = [];
    }

    internal class KeybindingCategoryViewModel(string name, string description, Icon icon, ReactiveCommand selectCommand)
    {
        public BindableReactiveProperty<string> Name { get; } = new(name);
        public BindableReactiveProperty<string> Description { get; } = new(description);
        public BindableReactiveProperty<Icon> Icon { get; } = new(icon);
        public ReactiveCommand SelectCommand { get; } = selectCommand;
    }
}
