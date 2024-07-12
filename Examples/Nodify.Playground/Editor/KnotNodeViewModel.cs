using System.Windows.Controls;

namespace Nodify.Playground
{
    public class KnotNodeViewModel : NodeViewModel
    {
        public KnotNodeViewModel(Orientation orientation)
        {
            Orientation = orientation;
        }

        public KnotNodeViewModel() : this(Orientation.Horizontal)
        {
        }

        private ConnectorViewModel _connector = default!;
        public ConnectorViewModel Connector
        {
            get => _connector;
            set
            {
                if (SetProperty(ref _connector, value))
                {
                    _connector.Node = this;
                }
            }
        }

        public ConnectorFlow Flow { get; set; }
    }
}
