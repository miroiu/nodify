using System.Linq;

namespace Nodify.Playground
{
    public enum ConnectorFlow
    {
        Input,
        Output
    }

    public class ConnectorViewModel : ObservableObject
    {
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private NodeViewModel _node = default!;
        public NodeViewModel Node
        {
            get => _node;
            internal set
            {
                if (SetProperty(ref _node, value))
                {
                    OnNodeChanged();
                }
            }
        }

        public ConnectorFlow Flow { get; private set; }

        public int MaxConnections { get; set; } = 2;

        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();

        protected virtual void OnNodeChanged()
        {
            if (Node is FlowNodeViewModel flow)
            {
                Flow = flow.Input.Contains(this) ? ConnectorFlow.Input : ConnectorFlow.Output;
            }
            else if (Node is KnotNodeViewModel knot)
            {
                Flow = knot.Flow;
            }
        }

        public bool IsConnectedTo(ConnectorViewModel con)
            => Connections.Any(c => c.Input == con || c.Output == con);

        public virtual bool AllowsNewConnections()
            => Connections.Count < MaxConnections;

        public void Disconnect()
            => Node.Graph.Schema.DisconnectConnector(this);
    }
}
