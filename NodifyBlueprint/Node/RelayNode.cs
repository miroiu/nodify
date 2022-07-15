namespace NodifyBlueprint
{
    public interface IRelayNode : IGraphElement
    {
        public IConnector Connector { get; }
    }

    public class RelayNode : GraphElement, IRelayNode
    {
        public RelayNode(IGraph graph) : base(graph)
        {
            Connector = new RelayConnector(this);
        }

        public IConnector Connector { get; }
    }
}
