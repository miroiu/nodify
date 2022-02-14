using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class EditorViewModel : ObservableObject
    {
        public event Action<EditorViewModel, CalculatorViewModel>? OnOpenInnerCalculator;

        public CalculatorViewModel Calculator { get; set; }
        public EditorViewModel? Parent { get; set; }

        public EditorViewModel()
        {
            Calculator = new CalculatorViewModel();
            OpenCalculatorCommand = new DelegateCommand<CalculatorViewModel>(calculator =>
            {
                OnOpenInnerCalculator?.Invoke(this, calculator);
            });
        }

        public INodifyCommand OpenCalculatorCommand { get; }

        public Guid Id { get; } = Guid.NewGuid();

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}
