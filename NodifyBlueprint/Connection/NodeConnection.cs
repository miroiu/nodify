namespace NodifyBlueprint
{
    public class NodeConnection : IConnection
    {
        public NodeConnection(IConnector source, IConnector target)
        {
            Source = source;
            Target = target;

            source.IsConnected = true;
            target.IsConnected = true;
        }

        public IConnector Source { get; }
        public IConnector Target { get; }
    }
}