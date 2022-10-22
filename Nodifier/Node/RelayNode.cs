namespace Nodifier
{
    public interface IRelayNode : IGraphElement, ICanDisconnect
    {
        public IConnector Connector { get; }
    }

    public class RelayNode : GraphElement, IRelayNode
    {
        public RelayNode(IGraphEditor graph) : base(graph)
        {
            Connector = new RelayConnector(this);
        }

        public IConnector Connector { get; }

        public void Disconnect()
            => Graph.Disconnect(Connector);
    }
}
