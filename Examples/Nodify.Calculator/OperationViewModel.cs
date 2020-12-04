using StringMath;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Nodify.Calculator
{
    public class OperationViewModel : ObservableObject
    {
        public OperationViewModel()
        {
            Input.WhenAdded(x =>
            {
                x.Operation = this;
                x.IsInput = true;
                x.PropertyChanged += OnInputValueChanged;
            })
            .WhenRemoved(x => 
            {
                x.PropertyChanged -= OnInputValueChanged;
            });
        }

        private void OnInputValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConnectorViewModel.Value))
            {
                OnInputValueChanged();
            }
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public IOperation? Operation { get; set; }

        public NodifyObservableCollection<ConnectorViewModel> Input { get; } = new NodifyObservableCollection<ConnectorViewModel>();

        private ConnectorViewModel? _output;
        public ConnectorViewModel? Output
        {
            get => _output;
            set
            {
                if (SetProperty(ref _output, value) && _output != null)
                {
                    _output.Operation = this;
                }
            }
        }

        protected virtual void OnInputValueChanged()
        {
            if (Output != null && Operation != null)
            {
                var input = Input.Select(i => i.Value).ToArray();
                Output.Value = Operation?.Execute(input) ?? 0;
            }
        }
    }

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

    public class ExpressionOperationViewModel : OperationViewModel
    {
        private string? _expression;
        public string? Expression
        {
            get => _expression;
            set => SetProperty(ref _expression, value)
                .Then(GenerateInput);
        }

        private void GenerateInput()
        {
            try
            {
                var operation = SMath.CreateOperation(Expression);
                var toRemove = Input.Where(i => !operation.Replacements.Contains(i.Title)).ToArray();
                toRemove.ForEach(i => Input.Remove(i));
                var left = Input.Select(s => s.Title).ToHashSet();

                foreach (var variable in operation.Replacements.Where(s => !left.Contains(s)))
                {
                    Input.Add(new ConnectorViewModel
                    {
                        Title = variable
                    });
                }

                OnInputValueChanged();
            }
            catch
            {

            }
        }

        protected override void OnInputValueChanged()
        {
            if (Output != null)
            {
                var repl = new Replacements();
                Input.ForEach(i => repl[i.Title] = i.Value);

                Output.Value = SMath.Evaluate(Expression, repl);
            }
        }
    }
}
