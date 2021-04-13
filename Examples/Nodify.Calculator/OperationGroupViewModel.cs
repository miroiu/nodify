namespace Nodify.Calculator
{
    public class OperationGroupViewModel : OperationViewModel
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
    }
}