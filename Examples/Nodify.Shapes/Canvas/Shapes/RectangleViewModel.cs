using System.Windows.Media;

namespace Nodify.Shapes.Canvas
{
    public class RectangleViewModel : ShapeViewModel
    {
        public RectangleViewModel()
        {
            Color = Color.FromRgb(63, 138, 226);
            Text = "Rectangle";
        }
    }
}
