namespace Nodify.Calculator
{
    public class CalculatorInputOperationViewModel : OperationViewModel
    {
        public CalculatorInputOperationViewModel()
        {
            AddOutputCommand = new RequeryCommand(
                () => Output.Add(new ConnectorViewModel
                {
                    Title = $"In {Output.Count}"
                }),
                () => Output.Count < 10);

            RemoveOutputCommand = new RequeryCommand(
                () => Output.RemoveAt(Output.Count - 1),
                () => Output.Count > 1);

            Output.Add(new ConnectorViewModel
            {
                Title = $"In {Output.Count}"
            });
        }

        public new NodifyObservableCollection<ConnectorViewModel> Output { get; set; } =
            new NodifyObservableCollection<ConnectorViewModel>();

        public INodifyCommand AddOutputCommand { get; }
        public INodifyCommand RemoveOutputCommand { get; }
    }
}
