namespace Nodify.Calculator
{
    public class ExpandoOperationViewModel : OperationViewModel
    {
        public ExpandoOperationViewModel()
        {
            AddInputCommand = new RequeryCommand(
                () => Input.Add(new ConnectorViewModel()),
                () => Input.Count < MaxInput);

            RemoveInputCommand = new RequeryCommand(
                () => Input.RemoveAt(Input.Count - 1),
                () => Input.Count > MinInput);

            Input.WhenAdded(_ => AddInputCommand.RaiseCanExecuteChanged());
            Input.WhenRemoved(_ => AddInputCommand.RaiseCanExecuteChanged());
            Input.WhenAdded(_ => RemoveInputCommand.RaiseCanExecuteChanged());
            Input.WhenRemoved(_ => RemoveInputCommand.RaiseCanExecuteChanged());
        }

        public INodifyCommand AddInputCommand { get; }
        public INodifyCommand RemoveInputCommand { get; }

        private uint _minInput = 0;
        public uint MinInput
        {
            get => _minInput;
            set => SetProperty(ref _minInput, value);
        }

        private uint _maxInput = uint.MaxValue;
        public uint MaxInput
        {
            get => _maxInput;
            set => SetProperty(ref _maxInput, value);
        }
    }
}
