using System.Windows;

namespace Nodify.Calculator
{
    public class OperationGroupViewModel : OperationViewModel
    {
        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
    }
}