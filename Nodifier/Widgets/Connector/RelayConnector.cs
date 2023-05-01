namespace Nodifier
{
    public interface IRelayConnector : IConnector
    {
        IConnector? Source { get; }
        IConnector? Target { get; }
    }

    public class RelayConnector : BaseConnector, IRelayConnector
    {
        public IConnector? Source { get; private set; } = default!;
        public IConnector? Target { get; private set; } = default!;

        public override void AddConnection(IConnection connection)
        {
            Source = connection.Source != this ? connection.Source : connection.Target;
            Target = connection.Target != this ? connection.Target : connection.Source;

            base.AddConnection(connection);
        }

        public override void RemoveConnection(IConnection connection)
        {
            Source = null;
            Target = null;

            base.RemoveConnection(connection);
        }

        public RelayConnector(IRelayNodeWidget node) : base(node)
        {
        }
    }
}
