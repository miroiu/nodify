namespace Nodify.Calculator
{
    public class ConnectionViewModel : ObservableObject
    {
        private ConnectorViewModel _input = default!;
        public ConnectorViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private ConnectorViewModel _output = default!;
        public ConnectorViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }
    }
}
