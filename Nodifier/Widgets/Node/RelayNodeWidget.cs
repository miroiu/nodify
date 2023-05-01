namespace Nodifier
{
    public interface IRelayNodeWidget : IGraphElement, ICanDisconnect
    {
        public IConnector Connector { get; }
    }

    public class RelayNodeWidget : GraphElement, IRelayNodeWidget
    {
        public RelayNodeWidget(IGraphWidget graph) : base(graph)
        {
            Connector = new RelayConnector(this);
        }

        public IConnector Connector { get; }

        public void Disconnect()
            => Graph.Disconnect(Connector);
    }
}
