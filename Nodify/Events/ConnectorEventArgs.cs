using System.Windows;

namespace Nodify
{
    public delegate void ConnectorEventHandler(object sender, ConnectorEventArgs e);

    public class ConnectorEventArgs : RoutedEventArgs
    {
        public ConnectorEventArgs(object sourceConnector)
        {
            Connector = sourceConnector;
        }

        public Point Anchor { get; set; }

        public object Connector { get; }
    }
}
