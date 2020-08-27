using System.Windows;

namespace Nodify
{
    public delegate void PendingConnectionEventHandler(object sender, PendingConnectionEventArgs e);

    public class PendingConnectionEventArgs : RoutedEventArgs
    {
        public PendingConnectionEventArgs(object sourceConnector)
        {
            SourceConnector = sourceConnector;
        }

        public Point Anchor { get; set; }

        public object SourceConnector { get; }
        public object? TargetConnector { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
    }
}
