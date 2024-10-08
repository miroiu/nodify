using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Playground
{
    public class NodifyEditorViewModel : ObservableObject
    {
        public NodifyEditorViewModel()
        {
            Schema = new GraphSchema();

            PendingConnection = new PendingConnectionViewModel
            {
                Graph = this
            };

            DeleteSelectionCommand = new DelegateCommand(DeleteSelection, () => SelectedNodes.Count > 0 || SelectedConnections.Count > 0);
            CommentSelectionCommand = new RequeryCommand(() => Schema.AddCommentAroundNodes(SelectedNodes, "New comment"), () => SelectedNodes.Count > 0);
            DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(c => c.Disconnect());
            CreateConnectionCommand = new DelegateCommand<object>(target => Schema.TryAddConnection(PendingConnection.Source!, target), target => PendingConnection.Source != null && target != null);

            Connections.WhenAdded(c =>
            {
                c.Graph = this;
                c.Input.Connections.Add(c);
                c.Output.Connections.Add(c);
            })
            // Called when the collection is cleared
            .WhenRemoved(c =>
            {
                c.Input.Connections.Remove(c);
                c.Output.Connections.Remove(c);
            });

            Nodes.WhenAdded(x => x.Graph = this)
                 // Not called when the collection is cleared
                 .WhenRemoved(x =>
                 {
                     if (x is FlowNodeViewModel flow)
                     {
                         flow.Disconnect();
                     }
                     else if (x is KnotNodeViewModel knot)
                     {
                         knot.Connector.Disconnect();
                     }
                 })
                 .WhenCleared(x => Connections.Clear());
        }

        private NodifyObservableCollection<NodeViewModel> _nodes = new NodifyObservableCollection<NodeViewModel>();
        public NodifyObservableCollection<NodeViewModel> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        private NodifyObservableCollection<NodeViewModel> _selectedNodes = new NodifyObservableCollection<NodeViewModel>();
        public NodifyObservableCollection<NodeViewModel> SelectedNodes
        {
            get => _selectedNodes;
            set => SetProperty(ref _selectedNodes, value);
        }

        private NodifyObservableCollection<ConnectionViewModel> _selectedConnections = new NodifyObservableCollection<ConnectionViewModel>();
        public NodifyObservableCollection<ConnectionViewModel> SelectedConnections
        {
            get => _selectedConnections;
            set => SetProperty(ref _selectedConnections, value);
        }

        private NodifyObservableCollection<ConnectionViewModel> _connections = new NodifyObservableCollection<ConnectionViewModel>();
        public NodifyObservableCollection<ConnectionViewModel> Connections
        {
            get => _connections;
            set => SetProperty(ref _connections, value);
        }

        private Size _viewportSize;
        public Size ViewportSize
        {
            get => _viewportSize;
            set => SetProperty(ref _viewportSize, value);
        }

        public PendingConnectionViewModel PendingConnection { get; }

        private ConnectionViewModel? _selectedConnection;
        public ConnectionViewModel? SelectedConnection
        {
            get => _selectedConnection;
            set => SetProperty(ref _selectedConnection, value);
        }

        private NodeViewModel? _selectedNode;
        public NodeViewModel? SelectedNode
        {
            get => _selectedNode;
            set => SetProperty(ref _selectedNode, value);
        }

        public GraphSchema Schema { get; }

        public ICommand DeleteSelectionCommand { get; }
        public ICommand DisconnectConnectorCommand { get; }
        public ICommand CreateConnectionCommand { get; }
        public ICommand CommentSelectionCommand { get; }

        private void DeleteSelection()
        {
            foreach (var connection in SelectedConnections.ToList())
            {
                connection.Remove();
            }

            var selected = SelectedNodes.ToList();

            for (int i = 0; i < selected.Count; i++)
            {
                Nodes.Remove(selected[i]);
            }
        }
    }
}
