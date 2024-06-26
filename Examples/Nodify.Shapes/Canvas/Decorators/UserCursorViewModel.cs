using System.Windows;
using System.Windows.Media;

namespace Nodify.Shapes.Canvas
{
    public class UserCursorViewModel : ObservableObject, ICanvasDecorator
    {
        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public string? Name { get; set; }

        public Color Color { get; set; }
    }
}
