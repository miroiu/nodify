using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public interface IConnector
    {
        IGraphElement Node { get; }
        Point Anchor { get; }
        string? Title { get; set; }
        bool IsConnected { get; }
        IReadOnlyCollection<IConnection> Connections { get; }

        void Disconnect();
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);
    }
}
