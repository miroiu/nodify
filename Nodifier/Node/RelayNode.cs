namespace Nodifier
{
    public interface IRelayNode : IGraphElement
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
    }
}
