using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <inheritdoc cref="MultiGesture.Match.Any" />
    public sealed class AnyGesture : MultiGesture
    {
        public AnyGesture(params InputGesture[] gestures) : base(Match.Any, gestures)
        {
        }
    }
}
