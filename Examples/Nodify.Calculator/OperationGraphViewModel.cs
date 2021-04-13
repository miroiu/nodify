using System.Windows;

namespace Nodify.Calculator
{
    public class OperationGraphViewModel : CalculatorOperationViewModel
    {
        private double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _prevHeight;
        private double _prevWidth;

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
                        Height = _prevHeight;
                        Width = _prevWidth;
                    }
                    else
                    {
                        _prevHeight = Height;
                        _prevWidth = Width;

                        Height = 55;
                        Width = 180;
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