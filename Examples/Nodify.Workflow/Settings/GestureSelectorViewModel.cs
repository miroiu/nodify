using MouseGesture = Nodify.Interactivity.MouseGesture;
using System.Windows.Input;
using Nodify.Interactivity;
using R3;
using SystemKey = System.Windows.Input.Key;

namespace Nodify.Workflow.Settings
{
    internal class GestureSelectorViewModel(InputGestureRef gestureRef)
    {
        public InputGestureRef GestureRef { get; } = gestureRef;
    }

    internal class KeyGestureSelectorViewModel : GestureSelectorViewModel
    {
        public BindableReactiveProperty<ModifierKeys> Modifier { get; } = new();
        public BindableReactiveProperty<SystemKey> Key { get; } = new();
        public BindableReactiveProperty<SystemKey> ComboTriggerKey { get; } = new();

        public KeyGestureSelectorViewModel(InputGestureRef gestureRef) : base(gestureRef)
        {
            Key.Value = gestureRef.Value switch
            {
                MultiGesture multiGesture => multiGesture.Gestures.OfType<KeyGesture>().FirstOrDefault()?.Key ?? SystemKey.None,
                KeyGesture keyGesture => keyGesture.Key,
                _ => SystemKey.None
            };

            Modifier.Value = gestureRef.Value switch
            {
                MultiGesture multiGesture => multiGesture.Gestures.OfType<KeyGesture>().FirstOrDefault()?.Modifiers ?? ModifierKeys.None,
                KeyGesture keyGesture => keyGesture.Modifiers,
                _ => ModifierKeys.None
            };

            ComboTriggerKey.Value = gestureRef.Value is KeyComboGesture comboGesture ? comboGesture.TriggerKey : SystemKey.None;

            Key.Subscribe(key => gestureRef.Value = ComboTriggerKey.Value is SystemKey.None ? new KeyGesture(key, Modifier.Value) : new KeyComboGesture(key, ComboTriggerKey.Value, Modifier.Value));
            Modifier.Subscribe(modifier => gestureRef.Value = ComboTriggerKey.Value is SystemKey.None ? new KeyGesture(Key.Value, modifier) : new KeyComboGesture(Key.Value, ComboTriggerKey.Value, modifier));

            ComboTriggerKey.Subscribe(triggerKey =>
            {
                if (triggerKey != SystemKey.None)
                {
                    gestureRef.Value = new KeyComboGesture(triggerKey, Key.Value, Modifier.Value);
                }
            });
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
        public BindableReactiveProperty<ModifierKeys> Modifier { get; } = new();

        public BindableReactiveProperty<SystemKey> Key { get; } = new();

        public MouseAction SelectedMouseAction
        {
            get => field;
            set
            {
                field = value;
                GestureRef.Value = new MouseGesture(value, Modifier.Value, Key.Value);
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
            Modifier.Value = gestureRef.Value switch
            {
                MouseGesture mouseGesture => mouseGesture.Modifiers,
                System.Windows.Input.MouseGesture sysMouseGesture => sysMouseGesture.Modifiers,
                MultiGesture multiGesture => multiGesture.Gestures.OfType<MouseGesture>().FirstOrDefault()?.Modifiers ?? ModifierKeys.None,
                _ => ModifierKeys.None
            };

            Key.Value = gestureRef.Value switch
            {
                MouseGesture mouseGesture => mouseGesture.Key,
                MultiGesture multiGesture => multiGesture.Gestures.OfType<MouseGesture>().FirstOrDefault()?.Key ?? SystemKey.None,
                _ => SystemKey.None
            };

            SelectedMouseAction = gestureRef.Value switch
            {
                MouseGesture mouseGesture => mouseGesture.MouseAction,
                System.Windows.Input.MouseGesture sysMouseGesture => sysMouseGesture.MouseAction,
                MultiGesture multiGesture => multiGesture.Gestures.OfType<MouseGesture>().FirstOrDefault()?.MouseAction ?? MouseAction.None,
                _ => MouseAction.None
            };
        }
    }
}
