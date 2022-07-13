using Stylet;
using System;

namespace NodifyBlueprint
{
    public class PendingConnection : PropertyChangedBase, IPendingConnection
    {
        public IGraph Graph { get; }
        private IConnector? _source;

        public PendingConnection(IGraph graph)
        {
            Graph = graph;
        }

        public virtual void Start(IConnector source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public virtual void Complete(object target)
        {
            if (_source == null)
            {
                throw new NullReferenceException("Must call Start() before calling Complete()");
            }

            // TODO: Maybe delegate the connection to the element itself
            if (target is IConnector connector)
            {
                Graph.TryConnect(_source, connector);
            }
            // TODO: Maybe delegate the connection to the element itself
            else if (target is IGraphElement element)
            {
                Graph.TryConnect(_source, element);
            }
        }
    }
}
