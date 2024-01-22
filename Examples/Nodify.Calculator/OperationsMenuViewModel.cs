using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify.Calculator
{
    public class OperationsMenuViewModel : ObservableObject
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                if (!value)
                {
                    Closed?.Invoke();
                }
            }
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public event Action? Closed;

        public void OpenAt(Point targetLocation)
        {
            Close();
            Location = targetLocation;
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
        }

        public NodifyObservableCollection<OperationInfoViewModel> AvailableOperations { get; }
        public INodifyCommand CreateOperationCommand { get; }
        private readonly CalculatorViewModel _calculator;

        public OperationsMenuViewModel(CalculatorViewModel calculator)
        {
            _calculator = calculator;
            List<OperationInfoViewModel> operations = new List<OperationInfoViewModel>
            {
                new OperationInfoViewModel
                {
                    Type = OperationType.Graph,
                    Title = "Operation Graph",
                },
                new OperationInfoViewModel
                {
                    Type = OperationType.Calculator,
                    Title = "Calculator"
                },
                new OperationInfoViewModel
                {
                    Type = OperationType.Expression,
                    Title = "Custom",
                }
            };
            operations.AddRange(OperationFactory.GetOperationsInfo(typeof(OperationsContainer)));

            AvailableOperations = new NodifyObservableCollection<OperationInfoViewModel>(operations);
            CreateOperationCommand = new DelegateCommand<OperationInfoViewModel>(CreateOperation);
        }

        private void CreateOperation(OperationInfoViewModel operationInfo)
        {
            OperationViewModel op = OperationFactory.GetOperation(operationInfo);
            op.Location = Location;

            _calculator.Operations.Add(op);

            var pending = _calculator.PendingConnection;
            if (pending.IsVisible)
            {
                var connector = pending.Source.IsInput ? op.Output : op.Input.FirstOrDefault();
                if (connector != null && _calculator.CanCreateConnection(pending.Source, connector))
                {
                    _calculator.CreateConnection(pending.Source, connector);
                }
            }
            Close();
        }
    }
}
