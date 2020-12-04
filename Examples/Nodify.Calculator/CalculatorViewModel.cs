using System.Linq;

namespace Nodify.Calculator
{
    public class CalculatorViewModel : ObservableObject
    {
        public CalculatorViewModel()
        {
            CreateConnectionCommand = new DelegateCommand<(object Source, object Target)>(target => CreateConnection((ConnectorViewModel)target.Source, (ConnectorViewModel)target.Target), target => CanCreateConnection((ConnectorViewModel)target.Source, target.Target as ConnectorViewModel));
            CreateOperationCommand = new DelegateCommand<CreateOperationInfoViewModel>(CreateOperation);

            UpdateAvailableOperations();

            Connections.WhenAdded(c =>
            {
                c.Input.IsConnected = true;
                c.Output.IsConnected = true;

                c.Input.Value = c.Output.Value;

                c.Output.PropertyChanged += (s, e) =>
                {
                    if(e.PropertyName == nameof(ConnectorViewModel.Value))
                    {
                        c.Input.Value = c.Output.Value;
                    }
                };
            })
            .WhenRemoved(c =>
            {
                var ic = Connections.Count(con => con.Input == c.Input || con.Output == c.Input);
                var oc = Connections.Count(con => con.Input == c.Output || con.Output == c.Output);

                if (ic == 0)
                {
                    c.Input.IsConnected = false;
                }

                if (oc == 0)
                {
                    c.Output.IsConnected = false;
                }
            });

            Operations.WhenAdded(x =>
            {
                x.Input.WhenRemoved(i =>
                {
                    var c = Connections.Where(con => con.Input == i || con.Output == i).ToArray();
                    c.ForEach(con => Connections.Remove(con));
                });
            });
        }

        private NodifyObservableCollection<OperationViewModel> _operations = new NodifyObservableCollection<OperationViewModel>();
        public NodifyObservableCollection<OperationViewModel> Operations
        {
            get => _operations;
            set => SetProperty(ref _operations, value);
        }

        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();

        public NodifyObservableCollection<OperationInfoViewModel> AvailableOperations { get; } = new NodifyObservableCollection<OperationInfoViewModel>();

        public INodifyCommand CreateConnectionCommand { get; }
        public INodifyCommand CreateOperationCommand { get; }

        private bool CanCreateConnection(ConnectorViewModel source, ConnectorViewModel? target)
            => target != null && source != target && source.Operation != target.Operation && source.IsInput != target.IsInput;

        private void CreateConnection(ConnectorViewModel source, ConnectorViewModel target)
            => Connections.Add(new ConnectionViewModel
            {
                Input = source.IsInput ? source : target,
                Output = target.IsInput ? source : target
            });

        private void CreateOperation(CreateOperationInfoViewModel arg)
        {
            var op = OperationFactory.GetOperation(arg.Info);
            op.Location = arg.Location;

            Operations.Add(op);
        }

        private void UpdateAvailableOperations()
        {
            AvailableOperations.Add(new OperationInfoViewModel
            {
                Type = OperationType.Expression,
                Title = "Custom"
            });
            var operations = OperationFactory.GetOperationsInfo(typeof(OperationsContainer));
            AvailableOperations.AddRange(operations);
        }
    }
}
