using System.Collections.Generic;
using System.Windows;

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

        private double _value;
        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value)
                .Then(() => ValueObservers.ForEach(o => o.Value = value));
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private bool _isInput;
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }

        private OperationViewModel _operation = default!;
        public OperationViewModel Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        public List<ConnectorViewModel> ValueObservers { get; } = new List<ConnectorViewModel>();
    }
}
