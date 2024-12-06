using System;
using System.Linq;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>Combines multiple input gestures.</summary>
    public class MultiGesture : InputGesture
    {
        public static readonly MultiGesture None = new MultiGesture(Match.Any);

        /// <summary>The strategy used by <see cref="Matches(object, InputEventArgs)"/>.</summary>
        public enum Match
        {
            /// <summary>At least one gesture must match.</summary>
            Any,
            /// <summary>All gestures must match.</summary>
            All
        }

        private readonly InputGesture[] _gestures;
        private readonly Match _match;

        /// <summary>Constructs an instance of a <see cref="MultiGesture"/>.</summary>
        /// <param name="match">The matching strategy.</param>
        /// <param name="gestures">The input gestures.</param>
        public MultiGesture(Match match, params InputGesture[] gestures)
        {
            _gestures = gestures;
            _match = match;
        }

        /// <inheritdoc />
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (_match == Match.Any)
            {
                return MatchesAny(targetElement, inputEventArgs);
            }

            return MatchesAll(targetElement, inputEventArgs);
        }

        private bool MatchesAll(object targetElement, InputEventArgs inputEventArgs)
        {
            for (int i = 0; i < _gestures.Length; i++)
            {
                if (!_gestures[i].Matches(targetElement, inputEventArgs))
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchesAny(object targetElement, InputEventArgs inputEventArgs)
        {
            for (int i = 0; i < _gestures.Length; i++)
            {
                if (_gestures[i].Matches(targetElement, inputEventArgs))
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <inheritdoc cref="MultiGesture.Match.Any" />
    public sealed class AnyGesture : MultiGesture
    {
        public AnyGesture(params InputGesture[] gestures) : base(Match.Any, gestures)
        {
        }
    }

    /// <inheritdoc cref="MultiGesture.Match.All" />
    public sealed class AllGestures : MultiGesture
    {
        public AllGestures(params InputGesture[] gestures) : base(Match.All, gestures)
        {
        }
    }

    /// <summary>
    /// An input gesture that allows changing its logic at runtime without changing its reference.
    /// Useful for classes that capture the object reference without the posibility of updating it. (e.g. <see cref="EditorCommands"/>)
    /// </summary>
    public sealed class InputGestureRef : InputGesture
    {
        /// <summary>The referenced gesture.</summary>
        public InputGesture Value { get; set; } = MultiGesture.None;

        private InputGestureRef() { }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            return Value.Matches(targetElement, inputEventArgs);
        }

        public static implicit operator InputGestureRef(MouseGesture gesture)
            => new InputGestureRef { Value = gesture };

        public static implicit operator InputGestureRef(System.Windows.Input.MouseGesture gesture)
            => new InputGestureRef { Value = gesture };

        public static implicit operator InputGestureRef(KeyGesture gesture)
            => new InputGestureRef { Value = gesture };

        public static implicit operator InputGestureRef(MultiGesture gesture)
            => new InputGestureRef { Value = gesture };
    }

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
        public bool IgnoreModifierKeysOnRelease { get; set; } = true;

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
        public MouseGesture(MouseAction action, ModifierKeys modifiers) : base(action, modifiers)
        {
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
