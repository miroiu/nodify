using System;
using System.Linq;

namespace NodifyBlueprint
{
    public interface IBlueprintGraph : IGraph
    {
        void Disconnect(IGraphNode node);
    }

    public class BlueprintGraph : Graph, IBlueprintGraph
    {
        private readonly IPendingConnection _pendingConnection;
        public override IPendingConnection PendingConnection => _pendingConnection;

        public BlueprintGraph() : base()
        {
            _pendingConnection = new BlueprintPendingConnection(this);
        }

        protected override IConnection CreateConnection(IConnector source, IConnector target)
        {
            return new BlueprintConnection(source, target);
        }

        protected override bool CanConnect(IConnector source, IConnector target)
        {
            bool result = base.CanConnect(source, target);

            //if (result)
            //{
            //    var srcType = GetValueType(source);
            //    var targetType = GetValueType(target);

            //    result = source is IOutputConnector && IsAssignable(srcType, targetType) || target is IOutputConnector && IsAssignable(targetType, srcType);
            //}

            result |= source is IRelayConnector || target is IRelayConnector;
            return result;
        }

        private static bool IsAssignable(Type? srcType, Type? targetType)
        {
            return targetType != null && targetType.IsAssignableFrom(srcType);
        }

        private static Type? GetValueType(IConnector conn)
        {
            var type = conn.GetType();
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            return type;
        }

        public virtual void Disconnect(IGraphNode node)
        {
            var inputConnections = node.Input.SelectMany(c => c.Connections);
            var outputConnections = node.Output.SelectMany(c => c.Connections);

            _connections.RemoveRange(inputConnections);
            _connections.RemoveRange(outputConnections);
        }
    }
}
