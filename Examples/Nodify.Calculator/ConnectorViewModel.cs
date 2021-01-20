namespace Nodify.Calculator
{
    public class ConnectorViewModel : ObservableObject
    {
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private decimal _value;
        public decimal Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _isInput;
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        private OperationViewModel _operation = default!;
        public OperationViewModel Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }
    }
}
