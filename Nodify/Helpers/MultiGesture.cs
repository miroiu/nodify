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
        public override bool Matches(object targetElement, EventArgs inputEventArgs)
        {
            if (_match == Match.Any)
            {
                return MatchesAny(targetElement, inputEventArgs);
            }

            return MatchesAll(targetElement, inputEventArgs);
        }

        private bool MatchesAll(object targetElement, EventArgs inputEventArgs)
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

        private bool MatchesAny(object targetElement, EventArgs inputEventArgs)
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

        public override bool Matches(object targetElement, EventArgs inputEventArgs)
        {
            return Value.Matches(targetElement, inputEventArgs);
        }

        public static implicit operator InputGestureRef(MouseGesture gesture)
            => new InputGestureRef { Value = gesture };

        public static implicit operator InputGestureRef(KeyGesture gesture)
            => new InputGestureRef { Value = new InputKeyGesture(gesture) };

        public static implicit operator InputGestureRef(InputKeyGesture gesture)
            => new InputGestureRef { Value = gesture };

        public static implicit operator InputGestureRef(MultiGesture gesture)
            => new InputGestureRef { Value = gesture };
    }
}
