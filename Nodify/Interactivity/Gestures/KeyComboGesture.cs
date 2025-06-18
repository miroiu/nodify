using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Represents a keyboard gesture that requires a trigger key to be held down
    /// before pressing a combo key. For example, press and hold Space, then press Left arrow.
    /// </summary>
    public class KeyComboGesture : KeyGesture
    {
        private static readonly WeakReferenceCollection<KeyComboGesture> _allCombos = new WeakReferenceCollection<KeyComboGesture>(16);

        private bool _isTriggerDown;
        private int _comboCounter;

        /// <summary>
        /// Gets a value indicating whether the combo gesture has been performed at least once.
        /// </summary>
        private bool HasBeenPerformedAtLeastOnce => _comboCounter > 0;

        /// <summary>
        /// Gets or sets the key that must be pressed first to activate this combo gesture.
        /// </summary>
        public Key TriggerKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the combo key can be repeatedly triggered
        /// without releasing the trigger key.
        /// </summary>
        public bool AllowRepeatingComboKey { get; set; }

        static KeyComboGesture()
        {
            EventManager.RegisterClassHandler(typeof(UIElement), UIElement.PreviewKeyUpEvent, new KeyEventHandler(HandleKeyUp), true);
            EventManager.RegisterClassHandler(typeof(UIElement), UIElement.LostKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(HandleFocusLost), true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyComboGesture"/> class with the specified trigger and combo keys.
        /// </summary>
        /// <param name="triggerKey">The key that must be pressed first.</param>
        /// <param name="comboKey">The combo key pressed while the trigger key is held.</param>
        public KeyComboGesture(Key triggerKey, Key comboKey) : this(triggerKey, comboKey, ModifierKeys.None, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyComboGesture"/> class with the specified trigger and combo keys and modifiers.
        /// </summary>
        /// <param name="triggerKey">The key that must be pressed first.</param>
        /// <param name="comboKey">The combo key pressed while the trigger key is held.</param>
        /// <param name="modifiers">Any modifier keys required for the combo key.</param>
        public KeyComboGesture(Key triggerKey, Key comboKey, ModifierKeys modifiers) : this(triggerKey, comboKey, modifiers, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyComboGesture"/> class with the specified trigger key,
        /// combo key, modifiers, and display string.
        /// </summary>
        /// <param name="triggerKey">The key that must be pressed first.</param>
        /// <param name="comboKey">The combo key pressed while the trigger key is held.</param>
        /// <param name="modifiers">Any modifier keys required for the combo key.</param>
        /// <param name="displayString">The display string representing the gesture.</param>
        public KeyComboGesture(Key triggerKey, Key comboKey, ModifierKeys modifiers, string displayString) : base(comboKey, modifiers, displayString)
        {
            TriggerKey = triggerKey;
            _allCombos.Add(this);
        }

        private static void HandleFocusLost(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (var combo in _allCombos)
            {
                combo.Reset();
            }
        }

        private static void HandleKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var combo in _allCombos)
            {
                if (e.Key == combo.TriggerKey)
                {
                    // We don't want to handle the event if only the trigger key was pressed.
                    if (combo.HasBeenPerformedAtLeastOnce)
                    {
                        e.Handled = true;
                    }
                    combo.Reset();
                }
            }
        }

        private void Reset()
        {
            _isTriggerDown = false;
            _comboCounter = 0;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (inputEventArgs is KeyEventArgs { IsDown: true } keyArgs)
            {
                if (keyArgs.Key == TriggerKey)
                {
                    _isTriggerDown = true;
                }

                // The combo key only triggers the combo on key down
                bool matches = _isTriggerDown && base.Matches(targetElement, inputEventArgs);
                if (!matches)
                {
                    return false;
                }

                _comboCounter++;

                if (!AllowRepeatingComboKey)
                {
                    _isTriggerDown = false;
                }

                return matches;
            }

            return false;
        }
    }
}
