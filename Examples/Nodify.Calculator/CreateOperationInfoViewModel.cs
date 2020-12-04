using System.Windows;

namespace Nodify.Calculator
{
    public class CreateOperationInfoViewModel
    {
        public CreateOperationInfoViewModel(OperationInfoViewModel info, Point location)
        {
            Info = info;
            Location = location;
        }

        public OperationInfoViewModel Info { get; }
        public Point Location { get; }
    }
}
