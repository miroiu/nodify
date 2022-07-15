using System.Collections.Generic;
using System.Windows;

namespace Nodifier
{
    public interface IGraph
    {
        IReadOnlyCollection<IGraphElement> Elements { get; }
        IReadOnlyCollection<IGraphElement> SelectedElements { get; }
        IReadOnlyCollection<IConnection> Connections { get; }

        IPendingConnection PendingConnection { get; }

        void AddElement(IGraphElement node);
        void AddElements(IEnumerable<IGraphElement> nodes);
        void RemoveElement(IGraphElement node);
        void RemoveElements(IEnumerable<IGraphElement> nodes);

        bool TryConnect(IConnector source, IConnector target);
        bool TryConnect(IConnector source, IGraphElement target);
        void Disconnect(IConnector connector);
        void Disconnect(IConnection connection);
        void Split(IConnection connection, Point location);
    }
}