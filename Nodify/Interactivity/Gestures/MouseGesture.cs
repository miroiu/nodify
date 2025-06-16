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

            return Keyboard.IsKeyDown(Key);
        }

        private static readonly Key[] _allKeys = new[]
        {
            // Alphanumeric
            Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J,
            Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T,
            Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z,
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,

            // Punctuation and symbols
            Key.Oem3, Key.OemMinus, Key.OemPlus, Key.OemOpenBrackets, Key.OemCloseBrackets,
            Key.Oem5, Key.Oem1, Key.OemQuotes, Key.OemComma, Key.OemPeriod, Key.Oem2,

            // Function keys
            Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8,
            Key.F9, Key.F10, Key.F11, Key.F12,

            // Navigation
            Key.Left, Key.Right, Key.Up, Key.Down,
            Key.PageUp, Key.PageDown, Key.Home, Key.End,

            // Editing
            Key.Back, Key.Delete, Key.Insert,

            // Special
            Key.Space, Key.Return, Key.Escape, Key.Tab,

            // Numeric keypad
            Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4,
            Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9,
            Key.Multiply, Key.Add, Key.Subtract, Key.Divide, Key.Decimal
        };

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
