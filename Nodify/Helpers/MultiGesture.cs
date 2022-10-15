using System.Windows.Input;

namespace Nodify
{
    /// <summary>Combines multiple input gestures.</summary>
    public class MultiGesture : InputGesture
    {
        /// <summary>The strategy used by <see cref="Matches(object, InputEventArgs)"/>.</summary>
        public enum Match
        {
            /// <summary>Any gesture can match.</summary>
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
}
