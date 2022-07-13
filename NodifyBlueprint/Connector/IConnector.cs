using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public interface IConnector
    {
        IGraphNode Node { get; }
        Point Anchor { get; }
        string? Title { get; set; }
        bool IsConnected { get; set; }
        IReadOnlyCollection<IConnection> Connections { get; }

        void Disconnect();
        void AddConnection(IConnection connection);
    }
}
