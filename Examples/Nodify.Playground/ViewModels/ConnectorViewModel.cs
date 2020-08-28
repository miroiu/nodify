using System.Windows;

namespace Nodify.Playground
{
    public enum ConnectorType
    {
        Data,
        Flow
    }

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

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
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

        private ConnectorType _type;
        public ConnectorType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public ConnectorFlow Flow { get; private set; }

        public int MaxConnections => Type == ConnectorType.Flow ? Flow == ConnectorFlow.Output ? 1 : int.MaxValue : Flow == ConnectorFlow.Input ? 1 : int.MaxValue;

        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();

        public ConnectorViewModel()
        {
            Connections.WhenAdded(c =>
            {
                c.Input.IsConnected = true;
                c.Output.IsConnected = true;
            }).WhenRemoved(c =>
            {
                if (c.Input.Connections.Count == 0)
                {
                    c.Input.IsConnected = false;
                }

                if (c.Output.Connections.Count == 0)
                {
                    c.Output.IsConnected = false;
                }
            });
        }

        protected virtual void OnNodeChanged()
        {
            if (Node is FlowNodeViewModel flow)
            {
                Flow = flow.Input.Contains(this) ? ConnectorFlow.Input : ConnectorFlow.Output;
            }
            else if (Node is KnotNodeViewModel knot)
            {
                Flow = knot.Connector.Type == ConnectorType.Flow ? ConnectorFlow.Input : ConnectorFlow.Output;
            }
        }

        public virtual bool AllowsNewConnections()
            => Connections.Count < MaxConnections;

        public void Disconnect()
            => Node.Graph.Schema.DisconnectConnector(this);
    }
}
