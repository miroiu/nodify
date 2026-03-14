using MouseGesture = Nodify.Interactivity.MouseGesture;
using System.Windows.Input;
using Nodify.Interactivity;
using R3;

namespace Nodify.Workflow.Settings
{
    internal class GestureSelectorViewModel(InputGestureRef gestureRef)
    {
        public InputGestureRef GestureRef { get; } = gestureRef;
    }

    internal class KeyGestureSelectorViewModel : GestureSelectorViewModel
    {
        public BindableReactiveProperty<ModifierKeys> Modifier { get; } = new();
        public BindableReactiveProperty<Key> Key { get; } = new();

        public KeyGestureSelectorViewModel(InputGestureRef gestureRef) : base(gestureRef)
        {
            Modifier.Value = gestureRef.Value switch
            {
                MultiGesture multiGesture => multiGesture.Gestures.OfType<KeyGesture>().FirstOrDefault()?.Modifiers ?? ModifierKeys.None,
                KeyGesture keyGesture => keyGesture.Modifiers,
                _ => ModifierKeys.None
            };

            Key.Value = gestureRef.Value switch
            {
                MultiGesture multiGesture => multiGesture.Gestures.OfType<KeyGesture>().FirstOrDefault()?.Key ?? System.Windows.Input.Key.None,
                KeyGesture keyGesture => keyGesture.Key,
                _ => System.Windows.Input.Key.None
            };
        }
    }

    internal class MouseActionViewModel(MouseAction action)
    {
        public MouseAction Action { get; } = action;

        public string Name => Action switch
        {
            MouseAction.LeftClick => "Left Click",
            MouseAction.RightClick => "Right Click",
            MouseAction.MiddleClick => "Middle Click",
            _ => Action.ToString()
        };
    }

    internal class MouseGestureSelectorViewModel : GestureSelectorViewModel
    {
        public MouseAction SelectedMouseAction
        {
            get => field;
            set
            {
                field = value;
                GestureRef.Value = new MouseGesture(value, ModifierKeys.None);
            }
        }

        public static MouseActionViewModel[] AvailableOptions { get; } =
        [
            new MouseActionViewModel(MouseAction.LeftClick),
            new MouseActionViewModel(MouseAction.RightClick),
            new MouseActionViewModel(MouseAction.MiddleClick),
        ];

        public MouseGestureSelectorViewModel(InputGestureRef gestureRef) : base(gestureRef)
        {
            SelectedMouseAction = gestureRef.Value switch
            {
                MultiGesture multiGesture => multiGesture.Gestures.OfType<MouseGesture>().FirstOrDefault()?.MouseAction ?? MouseAction.None,
                MouseGesture mouseGesture => mouseGesture.MouseAction,
                System.Windows.Input.MouseGesture sysMouseGesture => sysMouseGesture.MouseAction,
                _ => MouseAction.None
            };
        }
    }
}
