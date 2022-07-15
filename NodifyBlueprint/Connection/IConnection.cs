using System.Windows;

namespace NodifyBlueprint
{
    public interface IConnection
    {
        IGraph Graph { get; }

        IConnector Source { get; }
        IConnector Target { get; }

        void Split(Point location);
        void Disconnect();
    }
}