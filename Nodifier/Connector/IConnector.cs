using System.Collections.Generic;
using System.Windows;

namespace Nodifier
{
    public interface IConnector
    {
        IGraphElement Node { get; }
        Point Anchor { get; }
        bool IsConnected { get; }
        IReadOnlyCollection<IConnection> Connections { get; }

        void Disconnect();
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);

        bool TryConnectTo(IGraphElement element);
        bool TryConnectTo(IConnector other);
    }
}
