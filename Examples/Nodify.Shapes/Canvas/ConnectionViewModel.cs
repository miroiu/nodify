using System;

namespace Nodify.Shapes.Canvas
{
    public class ConnectionViewModel : IEquatable<ConnectionViewModel>
    {
        public ConnectionViewModel(ConnectorViewModel source, ConnectorViewModel target)
        {
            Source = source;
            Target = target;
        }

        public ConnectorViewModel Source { get; }
        public ConnectorViewModel Target { get; }

        public bool Equals(ConnectionViewModel? other)
            => other?.Source == Source && other.Target == Target;
    }
}
