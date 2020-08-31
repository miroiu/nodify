namespace Nodify.Playground
{
    public class KnotNodeViewModel : NodeViewModel
    {
        public KnotNodeViewModel()
        {
            IsCompact = true;
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
    }
}
