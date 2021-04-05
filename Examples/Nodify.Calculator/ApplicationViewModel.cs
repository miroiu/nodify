using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class ApplicationViewModel : ObservableObject
    {
        private readonly Stack<CalculatorViewModel> _calculators = new Stack<CalculatorViewModel>();

        public ApplicationViewModel()
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
    }
}
