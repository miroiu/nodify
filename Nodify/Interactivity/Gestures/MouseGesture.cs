using System;
using System.Linq;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Represents a mouse gesture that optionally includes a specific key press as part of the gesture.
    /// </summary>
    public sealed class MouseGesture : System.Windows.Input.MouseGesture
    {
        /// <summary>
        /// Gets or sets the key that must be pressed to match this gesture.
        /// </summary>
        public Key Key { get; set; }

        /// <summary>
        /// Whether to ignore modifier keys when releasing the mouse button.
        /// </summary>
        public bool IgnoreModifierKeysOnRelease { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseGesture"/> class with the specified mouse action, modifier keys, and a specific key.
        /// </summary>
        /// <param name="action">The action associated with this gesture.</param>
        /// <param name="modifiers">The modifiers associated with this gesture.</param>
        /// <param name="key">The key required to match the gesture.</param>
        public MouseGesture(MouseAction action, ModifierKeys modifiers, Key key) : base(action, modifiers)
        {
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseGesture"/> class with the specified mouse action and key.
        /// </summary>
        /// <param name="action">The action associated with this gesture.</param>
        /// <param name="key">The key required to match the gesture.</param>
        public MouseGesture(MouseAction action, Key key) : base(action)
        {
            Key = key;
        }

        /// <inheritdoc />
        public MouseGesture(MouseAction action, ModifierKeys modifiers)
            : base(action, modifiers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseGesture"/> class with the specified mouse action and modifier keys.
        /// </summary>
        /// <param name="action">The action associated with this gesture.</param>
        /// <param name="modifiers">The modifiers required to match the gesture.</param>
        /// <param name="ignoreModifierKeysOnRelease">Whether to ignore modifiers when releasing the mouse button.</param>
        public MouseGesture(MouseAction action, ModifierKeys modifiers, bool ignoreModifierKeysOnRelease)
            : base(action, modifiers)
        {
            IgnoreModifierKeysOnRelease = ignoreModifierKeysOnRelease;
        }

        /// <inheritdoc />
        public MouseGesture(MouseAction action) : base(action)
        {
        }

        /// <inheritdoc />
        public MouseGesture()
        {
        }

        /// <inheritdoc />
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (inputEventArgs is MouseButtonEventArgs || inputEventArgs is MouseWheelEventArgs)
            {
                bool matches = base.Matches(targetElement, inputEventArgs);

                if (IgnoreModifierKeysOnRelease && IsButtonReleased(inputEventArgs))
                {
                    ModifierKeys prevModifiers = Modifiers;
                    Modifiers = ModifierKeys.None;
                    matches |= base.Matches(targetElement, inputEventArgs);
                    Modifiers = prevModifiers;
                }

                return matches && MatchesKeyboard();
            }

            return false;
        }

        /// <summary>
        /// Checks whether the required key is pressed or no keys are pressed when <see cref="Key"/> is <see cref="Key.None"/>.
        /// </summary>
        private bool MatchesKeyboard()
        {
            if (Key is Key.None)
            {
                return !IsAnyKeyPressed();
            }

            return Keyboard.GetKeyStates(Key).HasFlag(KeyStates.Down);
        }

        private static readonly Key[] _allKeys = GetValidKeys();

        private static Key[] GetValidKeys()
        {
            var excludedKeys = new[]
            {
                Key.LeftCtrl, Key.RightCtrl,
                Key.LeftShift, Key.RightShift,
                Key.LeftAlt, Key.RightAlt,
                Key.LWin, Key.RWin,
                Key.None
            };

#if NET5_0_OR_GREATER
            return Enum.GetValues<Key>()
#else
            return Enum.GetValues(typeof(Key))
                .Cast<Key>()
#endif
                .Where(key => !excludedKeys.Contains(key))
                .ToArray();
        }

        /// <summary>
        /// Determines whether any key (excluding modifiers) is currently pressed.
        /// </summary>
        private static bool IsAnyKeyPressed()
            => _allKeys.Any(Keyboard.IsKeyDown);

        private static bool IsButtonReleased(InputEventArgs e)
        {
            if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Released)
                return true;

            if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Released)
                return true;

            return false;
        }
    }
}
