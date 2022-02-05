using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class EditorViewModel : ObservableObject
    {
        private readonly Stack<CalculatorViewModel> _calculators = new Stack<CalculatorViewModel>();

        public EditorViewModel()
        {
            _calculators.Push(new CalculatorViewModel());

            BackCommand = new RequeryCommand(() =>
            {
                _calculators.Pop();
                OnPropertyChanged(nameof(Current));
                OnPropertyChanged(nameof(IsCalculatorOpen));
            }, () => _calculators.Count > 1);

            OpenCalculatorCommand = new DelegateCommand<CalculatorViewModel>(calculator =>
            {
                _calculators.Push(calculator);
                OnPropertyChanged(nameof(Current));
                OnPropertyChanged(nameof(IsCalculatorOpen));
            });
        }

        public ICommand BackCommand { get; }
        public INodifyCommand OpenCalculatorCommand { get; }

        public CalculatorViewModel Current => _calculators.Peek();
        public bool IsCalculatorOpen => _calculators.Count > 1;
        public Guid Id { get; } = Guid.NewGuid();

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { SetProperty(ref _name,value); }
        }
    }
}
