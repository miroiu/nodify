using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// An input gesture that allows changing its logic at runtime without changing its reference.
    /// Useful for classes that capture the object reference without the posibility of updating it. (e.g. <see cref="EditorCommands"/>)
    /// </summary>
    public sealed class InputGestureRef : InputGesture
    {
        /// <summary>The referenced gesture.</summary>
        public InputGesture Value { get; set; } = MultiGesture.None;

        private InputGestureRef() { }

        internal InputGestureRef(InputGesture gesture)
        {
            Value = gesture;
        }

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

        /// <summary>
        /// Unbinds the current gesture.
        /// </summary>
        public void Unbind()
            => Value = MultiGesture.None;
    }

    /// <summary>
    /// Extension methods for the <see cref="InputGestureRef"/> class.
    /// </summary>
    public static class InputGestureRefExtensions
    {
        /// <summary>
        /// Creates a new <see cref="InputGestureRef"/> from the specified gesture.
        /// </summary>
        public static InputGestureRef AsRef(this InputGesture gesture)
        {
            return new InputGestureRef(gesture);
        }
    }
}
