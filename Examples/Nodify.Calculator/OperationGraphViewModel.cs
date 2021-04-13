using System.Windows;

namespace Nodify.Calculator
{
    public class OperationGraphViewModel : CalculatorOperationViewModel
    {
        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
        
        private Size _prevSize;

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (SetProperty(ref _isExpanded, value))
                {
                    if (_isExpanded)
                    {
                        Size = _prevSize;
                    }
                    else
                    {
                        _prevSize = Size;
                        // Fit content
                        Size = new Size(double.NaN, double.NaN);
                    }
                }
            }
        }

        public OperationGraphViewModel()
        {
            InnerCalculator.Operations[0].Location = new Point(50, 50);
            InnerCalculator.Operations[1].Location = new Point(200, 50);
        }
    }
}