using System.Linq;

namespace NodifyBlueprint
{
    public interface IBlueprintGraph : IGraph
    {
        void Disconnect(IConnection connection);
        void Disconnect(IGraphNode node);
    }

    public class BlueprintGraph : Graph, IBlueprintGraph
    {
        protected override IConnection CreateConnection(IConnector source, IConnector target)
        {
            return new BlueprintConnection(source, target);
        }

        private readonly IPendingConnection _pendingConnection;
        public override IPendingConnection PendingConnection => _pendingConnection;

        public BlueprintGraph() : base(BlueprintSchema.Default)
        {
            _pendingConnection = new BlueprintPendingConnection(this);
        }

        public override void TryConnect(IConnector source, IGraphElement target)
        {
            if (target is IGraphNode node)
            {
                IConnector? connector = node.Input.FirstOrDefault(x => Schema.CanConnect(source, x)) ?? node.Output.FirstOrDefault(x => Schema.CanConnect(source, x));
                if (connector != null)
                {
                    TryConnect(source, connector);
                }
            }
        }
    }
}
